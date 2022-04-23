using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        public Task<bool> IsExists(long id);
        public Task<List<Tag>> ListIdTitle(long? postId);
    }
}
