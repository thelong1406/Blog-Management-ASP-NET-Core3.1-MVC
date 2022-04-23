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
    public class PostCategoryRepository : ManyToManyRepository<PostCategory>, IPostCategoryRepository
    {

        public PostCategoryRepository(BlogManagementContext context) : base(context)
        {
        }
        public async Task<bool> IsExists(long postID, long categoryId)
        {
            return await context.PostCategories
                .AsNoTracking()
                .AnyAsync(t => t.PostId == postID && t.CategoryId == categoryId);
        }
        public async Task<List<PostCategory>> ListCategoriesOfPost(string slug, long? postId)
        {
            IQueryable<PostCategory> query = context.PostCategories
                .AsNoTracking()
                .Include(pc => pc.Category);

            if (postId.HasValue)
            {
                query = query.Where(pc=>pc.PostId.Equals(postId.Value));
            }
            else if(!(postId.HasValue && postId != null) || !string.IsNullOrWhiteSpace(slug))
            {
                query = query.Where(pc => pc.Post.Slug.ToLower() == slug.ToLower()) ;
            }
            return await query.Select(pc => new PostCategory
            {
                PostId = pc.PostId,
                CategoryId = pc.CategoryId,
                Category = pc.Category
            })
            .ToListAsync();
        }
    }
}
