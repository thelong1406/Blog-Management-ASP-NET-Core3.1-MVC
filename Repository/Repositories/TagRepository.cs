using DataAccess.Domain;
using DataAccess;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Repository.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(BlogManagementContext context) : base(context)
        {

        }
        public async Task<bool> IsExists(long id)
        {
            return await context.Tags.AsNoTracking().AnyAsync(t => t.Id == id);
        }
        public async Task<List<Tag>> ListIdTitle(long? postId)
        {
            IQueryable<Tag> query = context.Tags.AsNoTracking();
            // Execpt the categories of post [postId] already has
            if(postId.HasValue || postId != null)
            {
                List<long> postTagsId = await context.PostTags
                    .AsNoTracking()
                    .Where(pt => pt.PostId == postId.Value)
                    .Select(pt => pt.TagId)
                    .ToListAsync();
                query = query.Where(tag => !postTagsId.Contains(tag.Id));
            }
            return await query.Select(tag => 
            new Tag
            {
                Id = tag.Id,
                Title = tag.Title
            }).ToListAsync();
        }
    }
}
