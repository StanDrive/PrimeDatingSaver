using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Reports.Interfaces;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// LoggingController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/logging")]
    public class LoggingController : ApiController
    {
        private readonly ILogger _logger;

        private readonly IReportsBuilder _reportsBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="reportsBuilder">The reports builder.</param>
        public LoggingController(ILogger logger, IReportsBuilder reportsBuilder)
        {
            _logger = logger;

            _reportsBuilder = reportsBuilder;
        }

        /// <summary>
        /// Gets the today logs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("today")]
        public HttpResponseMessage GetTodayLogs()
        {
            _logger.Info("LoggingController.GetTodayLogs");

            try
            {
                var reportStream = _reportsBuilder.GetLoggingReports().GetTodaysLogs();

                reportStream.Position = 0;

                var response = new HttpResponseMessage { Content = new StreamContent(reportStream) };
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment") { FileName = $"Logs_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.xlsx" };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return response;
            }
            catch (Exception ex)
            {
                _logger.ErrorException($"LoggingController.GetTodayLogs Error: {ex.GetErrorMessage()}", ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Gets the logs by period.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"period/{startDate:regex(^\d{2}-\d{2}-\d{4}$)}/{endDate:regex(^\d{2}-\d{2}-\d{4}$)}")]
        public HttpResponseMessage GetLogsByPeriod(string startDate, string endDate)
        {
            _logger.Info($"ReportsFactory.GetLogsByPeriod [startPeriod: {startDate}, endPeriod: {endDate}]");

            const string dateFormat = "dd-MM-yyyy";
            try
            {
                if (!DateTime.TryParseExact(startDate, dateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var start))
                {
                    throw new ArgumentException($"Can't parse argument startDate: '{startDate}'. Format: {dateFormat}");
                }

                if (!DateTime.TryParseExact(endDate, dateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var end))
                {
                    throw new ArgumentException($"Can't parse argument endDate: '{endDate}'. Format: {dateFormat}");
                }

                var reportStream = _reportsBuilder.GetLoggingReports().GetLogsByPeriod(start, end);

                reportStream.Position = 0;

                var response = new HttpResponseMessage { Content = new StreamContent(reportStream) };
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment") { FileName = $"Logs_{start:dd-MM}_{end:dd-MM-yyyy}.xlsx" };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return response;
            }
            catch (ArgumentException ex)
            {
                var message = $"ReportsController.GetTranslatorsReport Error: {ex.GetErrorMessage()}";

                _logger.ErrorException(message, ex);

                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
            catch (Exception ex)
            {
                var message = $"ReportsController.GetTranslatorsReport Error: {ex.GetErrorMessage()}";

                _logger.ErrorException(message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }
        }


    }
}
