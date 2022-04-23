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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogManagementContext context) : base(context)
        {
        
        }

        //return Query Post
        public async Task<IQueryable<Post>> GetListPostsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string search,
            string userId,
            bool isAdmin,
            long? categoryId
        )
        {
            IQueryable<Post> query = context.Posts
                .AsNoTracking()
                .Select(post => new Post
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Summary = post.Summary,
                    CreatedAt = post.CreatedAt,
                    Thumbnail = post.Thumbnail,
                    MinutesSpentForReading = post.MinutesSpentForReading,
                    Published = post.Published,
                    IsDeleted = post.IsDeleted,
                    UserId = post.UserId,
                    Votes = post.Votes
                });

            //Filter
            //If userId has value => user logined => so they can see all blogs of them
            if (!string.IsNullOrEmpty(userId))
                query = query.Where(post => post.UserId == userId);
            else if (!isAdmin)
            {
                //This just allow others to see blogs that is published and has been deleted yet.
                query = query.Where(post => post.Published == true && post.IsDeleted == false);
            }
            //else show all 
                
            if(startDate.HasValue)
                query = query.Where(post => post.CreatedAt >= startDate.Value);
            if(startDate.HasValue)
                query = query.Where(post => post.CreatedAt <= endDate.Value);
            if(!string.IsNullOrEmpty(search))
                query = query.Where(post => post.Title.Contains(search));

            if (categoryId.HasValue)
            {
                //Get => categories id
                //of post
                //from PostCategories
                List<long> postIds = await context
                    .PostCategories
                    .AsNoTracking()
                    .Where(pc => pc.CategoryId == categoryId)
                    .Select(pc => pc.PostId)
                    .ToListAsync();

                if(postIds.Count > 0)
                    query = query.Where(post => postIds.Contains(post.PostId));
            }

            return query;
        }


        public async Task<Post> GetBySlugAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return await context.Posts.Where(t => t.Slug == slug).FirstOrDefaultAsync();
            }
            else
                return null;
        }
        public async Task<bool> IsExists(string slug, long? postId)
        {
            IQueryable<Post> query = context.Posts.AsNoTracking();
            if (postId.HasValue)
            {
                return await context.Posts.AsNoTracking().AnyAsync(t => t.PostId == postId.Value);
            }
            else if (!string.IsNullOrEmpty(slug))
            {
                return await context.Posts.AsNoTracking().AnyAsync(t => t.Slug == slug);
            }
            else
                return false;   
        }
        public async Task<bool> CheckAuthor(long postId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;
            else
            {
                if (await context.Posts.AsNoTracking().AnyAsync(t => t.PostId == postId))
                {
                    return await context.Posts.AsNoTracking().AnyAsync(t => t.PostId == postId && t.UserId == userId);
                }
                else
                    return false;
            }
        }

        public async Task<Post> GetAllPostDetail(string slug, long? postId)
        {
            IQueryable<Post> query = context.Posts.AsNoTracking();
            bool check = false;
            if (postId.HasValue)
            {
                check = true;
                query = query.Where(post => post.PostId.Equals(postId.Value));
            }
            else if (!string.IsNullOrEmpty(slug))
            {
                check = true;
                query = query.Where(post => post.Slug == slug);
            }
            if (check)
            {
                return await query.Include(post => post.ChildPosts)
                    .Include(post => post.PostComments).ThenInclude(pc => pc.User)
                    .Include(post => post.PostCategories).ThenInclude(pc => pc.Category)
                    .Include(post => post.PostTags).ThenInclude(pt => pt.Tag)
                    .Include(post => post.PostMetas)
                    .Include(post => post.User)
                    .Include(post => post.Votes)
                    .FirstOrDefaultAsync();
            }
            else
                return null;
        }
        public async Task<Post> GetPostForEdition(long postId)
        {
            return await context.Posts
                .AsNoTracking()
                .Where(post => post.PostId == postId)
                .Include(post => post.PostCategories).ThenInclude(pc => pc.Category)
                .Include(post => post.PostTags).ThenInclude(pc => pc.Tag)
                .Include(post => post.PostMetas)
                .FirstOrDefaultAsync();
        }

        private async Task<List<long>> GetChildsPost(long rootPostId)
        {
            List<long> collection = new List<long>();

            List<Post> childs = await context.Posts.Where(c => c.ParentId == rootPostId).ToListAsync();
            if (childs.Count > 0)
            {
                foreach (var post in childs)
                {
                    collection.Add(post.PostId);
                    collection.AddRange(await GetChildsPost(post.PostId));
                }
                return collection;
            }
            return collection;
        }

        public async Task<List<Post>> ListIdTitle (long postidToEditParent)
        {
            List<long> treePosts = await GetChildsPost(postidToEditParent);
            return await context.Posts
                .AsNoTracking()
                .Where(post => !treePosts.Contains(post.PostId) && post.PostId != postidToEditParent)
                .Select(post => new Post
                {
                    PostId = post.PostId,
                    Title = post.Title
                }).ToListAsync();
        }
    }
}
