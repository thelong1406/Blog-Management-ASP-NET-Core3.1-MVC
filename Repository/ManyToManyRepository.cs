using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ManyToManyRepository<TEntity> : IManyToManyRepository<TEntity> where TEntity : class
    {
        internal readonly BlogManagementContext context;
        internal DbSet<TEntity> dbSet;

        public ManyToManyRepository(BlogManagementContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> GetByIDAsync(object principalId, object dependentId)
        {
            return await dbSet.FindAsync(principalId, dependentId);
        }
        public virtual async Task InsertAsync(TEntity newEntity)
        {
            await dbSet.AddAsync(newEntity);
        }

        public virtual async Task DeleteAsync(object principalId, object dependentId)
        {
            TEntity entityToDelete = await dbSet.FindAsync(principalId, dependentId);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
    }
}
