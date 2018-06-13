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
    public abstract class BaseServiceController<TEntity> : ApiController
        where TEntity : class, IObjectState
    {
        private readonly IEntityService<TEntity> _entityService;

        private readonly ILogger _logger;

        protected BaseServiceController(IEntityService<TEntity> entityService, ILogger logger)
        {
            _entityService = entityService;

            _logger = logger;
        }

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
