using System.Collections.Generic;
using System.Linq;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer.Interfaces
{
    public interface IEntityService<TEntity> where TEntity : class, IObjectState
    {
        TEntity Find(params object[] keyValues);

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(params object[] keyValues);

        IQueryable<TEntity> Queryable();
    }
}
