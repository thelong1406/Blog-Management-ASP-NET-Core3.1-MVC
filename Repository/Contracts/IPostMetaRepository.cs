using DataAccess.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IPostMetaRepository : IGenericRepository<PostMeta>
    {
        Task<bool> IsExists(string key = null, long? postId = null, long? id = null);
        Task<List<PostMeta>> ListMetasOfPost(string slug, long? postId);
    }
}