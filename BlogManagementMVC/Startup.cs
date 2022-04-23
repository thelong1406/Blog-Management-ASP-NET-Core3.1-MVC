using DataAccess.Domain;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagementMVC.Configurations;
using Repository;
using Repository.Contracts;
using Repository.Repositories;
using BlogManagementMVC.Services;

namespace BlogManagementMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Db Context
            services.AddDbContext<BlogManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            
            #region Identity and Authentication
            //Identity user with role by BlogManagementContext
            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogManagementContext>();
            //Authentication
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                })
                .AddFacebook(options =>
                {
                    IConfigurationSection metaAuthNSection =
                        Configuration.GetSection("Authentication:Meta");

                    options.AppId = metaAuthNSection["AppId"];
                    options.AppSecret = metaAuthNSection["AppSecret"];
                });
            #endregion

            #region Transient
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Transient for send email
            services.AddTransient<IEmailSender>(s =>
                new EmailSender("localhost", 25, "no-reply@blog-management.com"));
            #endregion

            #region Scoped
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IManyToManyRepository<>), typeof(ManyToManyRepository<>));
            #endregion

            services.AddAutoMapper(typeof(MapperConfig));
            services.AddMvcCore();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); //Load Areas/Identity
            });
        }
    }
}
