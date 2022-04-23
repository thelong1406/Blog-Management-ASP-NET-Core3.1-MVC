using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task<IQueryable<Post>> GetListPostsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string search,
            string userId,
            bool isAdmin,
            long? categoryId
        );
        public Task<bool> IsExists(string slug, long? postId);
        public Task<bool> CheckAuthor(long postId, string userId);
        public Task<Post> GetBySlugAsync(string slug);

        public Task<Post> GetAllPostDetail(string slug, long? postId);
        public Task<Post> GetPostForEdition(long postId);
        public Task<List<Post>> ListIdTitle(long postidToEditParent);
    }
}
