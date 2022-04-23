using DataAccess.Domain;
using DataAccess;
using Repository.Contracts;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private BlogManagementContext _context;
        
        //DbSet
        public ITagRepository Tags { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IPostCategoryRepository PostCategories { get; private set; }
        public IPostTagRepository PostTags { get; private set; }
        public IVoteRepository Votes { get; private set; }
        public IPostCommentRepository PostComments { get; private set; }
        public IPostMetaRepository PostMetas { get; private set; }


        //Constructor
        public UnitOfWork(BlogManagementContext context)
        {
            this._context = context;
            this.Tags = new TagRepository(_context);
            this.Categories = new CategoryRepository(_context);
            this.Posts = new PostRepository(_context);
            this.PostCategories = new PostCategoryRepository(_context);
            this.PostTags = new PostTagRepository(_context);
            this.Votes = new VoteRepository(_context);
            this.PostComments = new PostCommentRepository(_context);
            this.PostMetas = new PostMetaRepository(_context);
        }

        //Method
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
