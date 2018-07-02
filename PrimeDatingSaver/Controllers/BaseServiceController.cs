using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.Web.Http.ApiController" />
    public abstract class BaseServiceController<TEntity> : ApiController
            where TEntity : class, IObjectState
    {
        private readonly IEntityService<TEntity> _entityService;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseServiceController{TEntity}"/> class.
        /// </summary>
        /// <param name="entityService">The entity service.</param>
        /// <param name="logger">The logger.</param>
        protected BaseServiceController(IEntityService<TEntity> entityService, ILogger logger)
        {
            _entityService = entityService;

            _logger = logger;
        }

        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage Find(params object[] keyValues)
        {
            var values = keyValues != null && keyValues.Any()
                ? keyValues.Aggregate(string.Empty, (s, i) => s + " " + i)
                : string.Empty;

            _logger.Info($"{typeof(TEntity).Name}Controller.Find [KeyValues: {values}]");

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _entityService.Find(keyValues));
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Selects the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage SelectQuery(string query)
        {
            _logger.Info($"{typeof(TEntity).Name}Controller.SelectQuery [Query: {query}]");

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _entityService.SelectQuery(query).ToList());
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage Insert(TEntity entity)
        {
            _logger.Info($"{typeof(TEntity).Name}Controller.Insert");

            try
            {
                _entityService.Insert(entity);

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage InsertRange(List<TEntity> entities)
        {
            _logger.Info($"{typeof(TEntity).Name}Controller.InsertRange");

            try
            {
                _entityService.InsertRange(entities);

                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage Update(TEntity entity)
        {
            _logger.Info($"{typeof(TEntity).Name}Controller.Update");

            try
            {
                _entityService.Update(entity);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Deletes the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public virtual HttpResponseMessage Delete(params object[] keyValues)
        {
            var values = keyValues != null && keyValues.Any()
                ? keyValues.Aggregate(string.Empty, (s, i) => s + " " + i)
                : string.Empty;

            _logger.Info($"{typeof(TEntity).Name}Controller.Update [KeyValues: {values}]");

            try
            {
                _entityService.Delete(keyValues);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual HttpResponseMessage GetAll()
        {
            _logger.Info($"{typeof(TEntity).Name}Controller.GetAll");

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _entityService.Queryable().ToList());
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }
    }
}
