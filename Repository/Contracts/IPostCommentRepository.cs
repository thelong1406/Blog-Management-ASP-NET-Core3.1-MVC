using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        public Task<bool> IsExists(long postCommentId);
    }
}
