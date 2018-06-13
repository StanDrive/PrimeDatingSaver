using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeDating.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IObjectState
    {
        TEntity Find(params object[] keyValues);

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(params object[] keyValues);

        void Delete(TEntity entity);

        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);

        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        IQueryFluent<TEntity> Query();

        IQueryable<TEntity> Queryable();
    }
}
