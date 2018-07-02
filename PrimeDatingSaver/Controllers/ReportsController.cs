using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Reports;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// ReportsController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        private readonly ILogger _logger;

        private readonly IReportsBuilder _reportsBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController" /> class.
        /// </summary>
        /// <param name="reportsBuilder">The reports builder.</param>
        /// <param name="logger">The logger.</param>
        public ReportsController(IReportsBuilder reportsBuilder, ILogger logger)
        {
            _reportsBuilder = reportsBuilder;

            _logger = logger;
        }

        /// <summary>
        /// Gets the translators report.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        [HttpGet]
        [Route(@"translators/{startDate:regex(^\d{2}-\d{2}-\d{4}$)}/{endDate:regex(^\d{2}-\d{2}-\d{4}$)}")]
        public HttpResponseMessage GetTranslatorsReport(string startDate, string endDate)
        {
            _logger.Debug($"ReportsFactory.GetTranslatorsReport [startPeriod: {startDate}, endPeriod: {endDate}]");

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

                var reportStream = _reportsBuilder.GetTranslatorsReports().TranslatorsReport(start, end);

                reportStream.Position = 0;

                var response = new HttpResponseMessage { Content = new StreamContent(reportStream) };
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment") { FileName = $"Translators_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.xlsx" };
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

        /// <summary>
        /// Gets the girls report.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        [HttpGet]
        [Route(@"girls/{startDate:regex(^\d{2}-\d{2}-\d{4}$)}/{endDate:regex(^\d{2}-\d{2}-\d{4}$)}")]
        public HttpResponseMessage GetGirlsReport(string startDate, string endDate)
        {
            _logger.Debug($"ReportsFactory.GetGirlsReport [startPeriod: {startDate}, endPeriod: {endDate}]");

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

                var reportStream = _reportsBuilder.GetGirlsReports().GirlsReport(start, end);

                reportStream.Position = 0;

                var response = new HttpResponseMessage { Content = new StreamContent(reportStream) };
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment") { FileName = $"Girls_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.xlsx" };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                return response;
            }
            catch (ArgumentException ex)
            {
                var message = $"ReportsController.GetGirlsReport Error: {ex.GetErrorMessage()}";

                _logger.ErrorException(message, ex);

                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }
            catch (Exception ex)
            {
                var message = $"ReportsController.GetGirlsReport Error: {ex.GetErrorMessage()}";

                _logger.ErrorException(message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}

