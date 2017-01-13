using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Queries.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(params object[] keyValues);
        IEnumerable<TEntity> ListAll();
        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entityToAdd);
        void AddRange(IEnumerable<TEntity> entitiesToAdd);
        void Remove(TEntity entityToRemove);
        void RemoveRange(IEnumerable<TEntity> entitiesToRemove);
    }
}
