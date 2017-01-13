using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Queries.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _ctx;

        public Repository(DbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(TEntity entityToAdd)
        {
            _ctx.Set<TEntity>().Add(entityToAdd);            
        }

        public void AddRange(IEnumerable<TEntity> entitiesToAdd)
        {
            _ctx.Set<TEntity>().AddRange(entitiesToAdd);
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return _ctx.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity GetById(params object[] keyValues)
        {
            return _ctx.Set<TEntity>().Find(keyValues);
        }

        public IEnumerable<TEntity> ListAll()
        {
            return _ctx.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entityToRemove)
        {
            _ctx.Set<TEntity>().Remove(entityToRemove);
        }

        public void RemoveRange(IEnumerable<TEntity> entitiesToRemove)
        {
            _ctx.Set<TEntity>().RemoveRange(entitiesToRemove);
        }
    }
}
