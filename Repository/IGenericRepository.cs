using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task DeleteAsync(object id);
        public void Delete(TEntity entityToDelete);
        public Task<IEnumerable<TEntity>> GetAsync
            (
                Expression<Func<TEntity, bool>> filter = null, 
                Func<IQueryable<TEntity>, 
                IOrderedQueryable<TEntity>> orderBy = null, 
                string includeProperties = ""
            );
        public Task<TEntity> GetByIDAsync(object id);
        public Task InsertAsync(TEntity newEntity);
        public void Update(TEntity entityToUpdate);
        public TEntity CheckUpdateObject(TEntity originalObj, TEntity updateObj);
    }
}