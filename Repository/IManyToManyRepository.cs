using System.Threading.Tasks;

namespace Repository
{
    public interface IManyToManyRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> GetByIDAsync(object principalId, object dependentId);
        public Task InsertAsync(TEntity newEntity);
        public  Task DeleteAsync(object principalId, object dependentId);
        public void Delete(TEntity entityToDelete);
    }
}