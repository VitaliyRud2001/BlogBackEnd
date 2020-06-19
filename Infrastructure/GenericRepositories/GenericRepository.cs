using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericRepositories
{
    public class GenericRepository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class, IEntityBase
    {
        protected readonly BlogContext Context;
        protected DbSet<TEntity> Entity;

        public GenericRepository(BlogContext blogContext)
        {
            Context = blogContext;
            Entity = Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Entity.AsQueryable();
        }
        public virtual void Add(TEntity entity)
        {
            Entity.Add(entity);
        }
        public async Task<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition)
        {
            return await Entity.FirstOrDefaultAsync(condition);
        }

        public void Remove(TEntity entity)
        {
            Entity.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            Entity.Update(entity);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }



        #region Dispose pattern
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
