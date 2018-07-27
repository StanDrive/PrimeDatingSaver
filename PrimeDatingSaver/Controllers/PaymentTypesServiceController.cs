﻿using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models.Database;
using PrimeDatingSaver.Filters;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// PaymentTypesServiceController
    /// </summary>
    [RoutePrefix("api/paymenttypes")]
    [BasicAuthenticationFilter]
    public class PaymentTypesServiceController : BaseServiceController<PaymentTypes>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentTypesServiceController"/> class.
        /// </summary>
        /// <param name="entityService">The entity service.</param>
        /// <param name="logger">The logger.</param>
        public PaymentTypesServiceController(IEntityService<PaymentTypes> entityService, ILogger logger)
            : base(entityService, logger)
        {   
        }

        /// <summary>
        /// Finds entity by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("find")]
        public HttpResponseMessage Find(int id)
        {
            return base.Find(id);
        }

        /// <summary>
        /// Select entities by query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("selectquery")]
        public override HttpResponseMessage SelectQuery(string query)
        {
            return base.SelectQuery(query);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public override HttpResponseMessage Insert(PaymentTypes entity)
        {
            return base.Insert(entity);
        }

        /// <summary>
        /// Inserts the range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertrange")]
        public override HttpResponseMessage InsertRange(List<PaymentTypes> entities)
        {
            return base.InsertRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public override HttpResponseMessage Update(PaymentTypes entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// Deletes the specified entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(int id)
        {
            return base.Delete(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public override HttpResponseMessage GetAll()
        {
            return base.GetAll();
        }
    }
}
