using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Contracts;
using DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository.Repositories
{
    public class PostTagRepository : ManyToManyRepository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(BlogManagementContext context) : base(context)
        {
        }
        public async Task<bool> IsExists(long postID, long tagId)
        {
            return await context.PostTags
                .AsNoTracking()
                .AnyAsync(t => t.PostId == postID && t.TagId == tagId);
        }

        public async Task<List<PostTag>> ListTagsOfPost(string slug, long? postId)
        {
            IQueryable<PostTag> query = context.PostTags
                .AsNoTracking()
                .Include(pc => pc.Tag);

            if (postId.HasValue)
            {
                query = query.Where(pt => pt.PostId.Equals(postId.Value));
            }
            else if (!(postId.HasValue && postId != null) || !string.IsNullOrWhiteSpace(slug))
            {
                query = query.Where(pt => pt.Post.Slug.ToLower() == slug.ToLower());
            }
            return await query.Select(pt => new PostTag
            {
                PostId = pt.PostId,
                TagId = pt.TagId,
                Tag = pt.Tag
            })
            .ToListAsync();
        }
    }
}
