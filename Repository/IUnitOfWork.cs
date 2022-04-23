using DataAccess.Domain;
using Repository.Contracts;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork
    {
        public ITagRepository Tags { get; }
        public ICategoryRepository Categories { get; }
        public IPostRepository Posts { get; }
        public IPostCategoryRepository PostCategories { get; }
        public IPostTagRepository PostTags { get; }
        public IVoteRepository Votes { get; }
        public IPostCommentRepository PostComments { get; }
        public IPostMetaRepository PostMetas { get; }
        public void Dispose();
        public Task SaveAsync();
    }
}