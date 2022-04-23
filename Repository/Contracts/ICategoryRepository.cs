using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<bool> IsExists(long id);
        public Task<bool> IsTitleExists(long id, string title);
        public Task<List<Category>> ListIdTitle(long? excategoryIdToEditItParentceptId = null, long? postId = null);
        public Task<Category> GetAndChildsAsync(long id);
    }
    
}
