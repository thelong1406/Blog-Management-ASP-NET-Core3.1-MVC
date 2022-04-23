using DataAccess;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class VoteRepository : ManyToManyRepository<Vote>, IVoteRepository
    {
        public VoteRepository(BlogManagementContext context) : base(context)
        {

        }
        public async Task<bool> IsExists(long postID, string userId)
        {
            return await context.Votes
                .AsNoTracking()
                .AnyAsync(t => t.PostId == postID && t.UserId == userId);
        }
        public void Update(Vote vote)
        {
            dbSet.Attach(vote);
            context.Entry(vote).State = EntityState.Modified;
        }
    }
}
