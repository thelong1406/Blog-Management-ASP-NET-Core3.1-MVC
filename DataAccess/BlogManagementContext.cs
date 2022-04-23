using System;
using DataAccess.Domain;
using DataAccess.Persistence.EntitiesConfigurations;
using DataAccess.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess
{
    public class BlogManagementContext : IdentityDbContext<User>
    {
        public BlogManagementContext()
        {
        }

        public BlogManagementContext(DbContextOptions<BlogManagementContext> options)
            : base(options)
        {

        }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMeta> PostMetas { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vote> Votes { get; set; }
        #endregion

        #region Model Creating & Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Model config
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostMetaConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceProductConfiguration());
            modelBuilder.ApplyConfiguration(new VoteConfiguration());

            //Seed Data
            modelBuilder.ApplyConfiguration(new SeedExampleRoles());
            modelBuilder.ApplyConfiguration(new SeedExampleUser());
            modelBuilder.ApplyConfiguration(new SeedExampleUserRole());

        }
        #endregion
    }
}
