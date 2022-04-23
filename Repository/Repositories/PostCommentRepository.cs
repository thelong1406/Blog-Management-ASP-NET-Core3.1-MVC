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
    public class PostCommentRepository : GenericRepository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(BlogManagementContext context) : base(context)
        {
        }
        public async Task<bool> IsExists (long postCommentId)
        {
            return await context.PostComments.AnyAsync(pc => pc.PostCommentId == postCommentId);
        }
    }
}
