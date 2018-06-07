using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models;

namespace PrimeDatingSaver.Controllers
{
    public class DataSaverController : ApiController
    {
        private readonly IDailyDataService _dailyDataService;

        private readonly ILogger _logger;

        public DataSaverController(IDailyDataService dailyDataService, ILogger logger)
        {
            _dailyDataService = dailyDataService;

            _logger = logger;
        }

        [HttpPost]
        public HttpResponseMessage UploadDailyData(DailyDataDto data)
        {
            _logger.Info($"DataSaverController.UploadDailyData");

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
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }

        }
    }
}
