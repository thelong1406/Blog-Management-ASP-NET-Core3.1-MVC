using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IVoteRepository : IManyToManyRepository<Vote>
    {
        public Task<bool> IsExists(long postID, string userId);
        public void Update(Vote vote);
    }
}
