using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IPostCategoryRepository : IManyToManyRepository<PostCategory>
    {
        public Task<bool> IsExists(long postID, long categoryId);
        public Task<List<PostCategory>> ListCategoriesOfPost(string slug, long? postId);
    }
}
