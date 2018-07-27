using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models;
using PrimeDatingSaver.Filters;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// DataSaverController
    /// </summary>
    [RoutePrefix("api/datasaver")]
    [BasicAuthenticationFilter]
    public class DataSaverController : ApiController
    {
        private readonly IDailyDataService _dailyDataService;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSaverController"/> class.
        /// </summary>
        /// <param name="dailyDataService">The daily data service.</param>
        /// <param name="logger">The logger.</param>
        public DataSaverController(IDailyDataService dailyDataService, ILogger logger)
        {
            _dailyDataService = dailyDataService;

            _logger = logger;
        }

        /// <summary>
        /// Uploads the daily data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadDailyData")]
        public HttpResponseMessage UploadDailyData(DailyDataDto data)
        {
            _logger.Info("DataSaverController.UploadDailyData");

            try
            {
                _dailyDataService.UpdateDailyData(data);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (PrimeDatingException ex)
            {
                _logger.WarningException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.GetErrorMessage());
            }
            catch (DbEntityValidationException ex)
            {
                var message = new StringBuilder();

                message.AppendLine("Validation Error:");

                foreach (var validationError in ex.EntityValidationErrors.SelectMany(t=> t.ValidationErrors))
                {
                    message.AppendLine(
                        $"PropertyName: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                }

                _logger.ErrorException($"{ex.Message}. {message}", ex);

                return Request.CreateResponse(HttpStatusCode.BadRequest, $"{ex.GetErrorMessage()}. {message}");
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }
    }
}
