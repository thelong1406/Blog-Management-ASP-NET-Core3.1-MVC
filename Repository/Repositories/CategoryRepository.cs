using DataAccess;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogManagementContext context) : base(context)
        {

        }
        
        public async Task<bool> IsExists(long id)
        {
            return await context.Categories.AsNoTracking().AnyAsync(t => t.Id == id);
        }
        
        public async Task<bool> IsTitleExists(long id, string title)
        {
            return await context.Categories
                .AsNoTracking()
                .AnyAsync(t => t.Title.ToLower() == title.ToLower() && t.Id != id);
        }
        private async Task<List<long>> GetChildsCategories(long rootCategoryId)
        {
            List<long> collection = new List<long>();

            List<Category> childs = await context.Categories.Where(c => c.ParentId == rootCategoryId).ToListAsync();
            if(childs.Count > 0)
            {
                foreach(var cate in childs)
                {
                    collection.Add(cate.Id);
                    collection.AddRange(await GetChildsCategories(cate.Id));
                }
                return collection;
            }
            return collection;
        }
        public async Task<List<Category>> ListIdTitle(long? categoryIdToEditItParent = null, long? postId = null)
        {
            IQueryable<Category> query = context.Categories;
            
            //Query to collect all parent categories available
            if(categoryIdToEditItParent.HasValue || categoryIdToEditItParent != null)
            {
                List<long> treeCategories = await GetChildsCategories(categoryIdToEditItParent.Value);
                query = query.Where(cate => !treeCategories.Contains(cate.Id) && cate.Id != categoryIdToEditItParent.Value);
            }
                
            
            // Execpt the categories of post [postId] already has
            if (postId.HasValue || postId != null)
            {
                List<long> postCategoriesId = await context.PostCategories
                    .AsNoTracking()
                    .Where(pc => pc.PostId.Equals(postId.Value))
                    .Select(pc => pc.CategoryId)
                    .ToListAsync();
                query = query.Where(cate => !postCategoriesId.Contains(cate.Id));
            }


            return await query.AsNoTracking()
                .Select(category => new Category
                {
                    Id = category.Id,
                    Title = category.Title
                }).ToListAsync();
        }
        public async Task<Category> GetAndChildsAsync(long id)
        {
            return await context.Categories
                .Include(cate => cate.ChildCategories)
                .FirstOrDefaultAsync(cate => cate.Id == id);
        }

    }
}
