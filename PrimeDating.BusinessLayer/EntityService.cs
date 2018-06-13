using System.Collections.Generic;
using System.Linq;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class, IObjectState
    {
        private readonly IRepository<TEntity> _repository;

        public EntityService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _repository.SelectQuery(query, parameters);
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _repository.InsertRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(params object[] keyValues)
        {
            _repository.Delete(keyValues);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _repository.Queryable();
        }
    }
}
