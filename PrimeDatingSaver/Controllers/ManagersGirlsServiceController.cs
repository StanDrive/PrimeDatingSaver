using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models.Database;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// ManagersGirlsServiceController
    /// </summary>
    [RoutePrefix("api/managersgirls")]
    public class ManagersGirlsServiceController : BaseServiceController<ManagersGirls>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagersGirlsServiceController"/> class.
        /// </summary>
        /// <param name="entityService">The entity service.</param>
        /// <param name="logger">The logger.</param>
        public ManagersGirlsServiceController(IEntityService<ManagersGirls> entityService, ILogger logger)
            : base(entityService, logger)
        {            
        }

        /// <summary>
        /// Finds entity by specified identifier.
        /// </summary>
        /// <param name="girlId">The girl identifier.</param>
        /// <param name="managerId">The manager identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("find")]
        public HttpResponseMessage Find(int girlId, int managerId)
        {
            return base.Find(girlId, managerId);
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
        public override HttpResponseMessage Insert(ManagersGirls entity)
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
        public override HttpResponseMessage InsertRange(List<ManagersGirls> entities)
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
        public override HttpResponseMessage Update(ManagersGirls entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// Deletes the specified entity by identifier.
        /// </summary>
        /// <param name="girlId">The girl identifier.</param>
        /// <param name="managerId">The manager identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(int girlId, int managerId)
        {
            return base.Delete(girlId, managerId);
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
