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
    public class PostMetaRepository : GenericRepository<PostMeta>, IPostMetaRepository
    {
        public PostMetaRepository(BlogManagementContext context) : base(context)
        {
        }
        public async Task<bool> IsExists(string key = null, long? postId = null, long? id = null)
        {
            if (key != null && postId.HasValue)
                return await context.PostMetas
                    .AsNoTracking()
                    .AnyAsync(pm => pm.Key == key && pm.PostId == postId.Value);

            else if(id.HasValue)
                return await context.PostMetas
                    .AsNoTracking()
                    .AnyAsync(pm => pm.Id == id.Value);
            
            else
                return false;
        }

        public async Task<List<PostMeta>> ListMetasOfPost(string slug, long? postId)
        {
            IQueryable<PostMeta> query = context.PostMetas.AsNoTracking();

            if (postId.HasValue)
            {
                query = query.Where(pt => pt.PostId.Equals(postId.Value));
            }
            else if (!(postId.HasValue && postId != null) || !string.IsNullOrWhiteSpace(slug))
            {
                query = query.Where(pt => string.Compare(pt.Post.Slug.ToLower(), slug.Trim().ToLower()) == 0);
            }
            return await query.Select(pm => new PostMeta
            {
                PostId = pm.PostId,
                Id = pm.Id,
                Key = pm.Key,
                Content = pm.Content,
            })
            .ToListAsync();
        }
    }
}
