using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IPostTagRepository : IManyToManyRepository<PostTag>
    {
        public Task<bool> IsExists(long postID, long tagId);
        public Task<List<PostTag>> ListTagsOfPost(string slug, long? postId);
    }
}
