using PrimeDating.Reports.Interfaces;

namespace PrimeDating.Reports
{
    internal class ReportsBuilder : IReportsBuilder
    {
        private readonly ITranslatorsReports _translatorsReports;

        private readonly IGirlsReports _girlsReports;

        private readonly ILoggingReports _loggingReports;

        private readonly IReportsForHeadsOfQuestionnaire _reportsForHeadsOfQuestionnaire;

        private readonly IAdminAreasReports _adminAreasReports;

        public ReportsBuilder(ITranslatorsReports translatorsReports, IGirlsReports girlsReports,
            ILoggingReports loggingReports, IReportsForHeadsOfQuestionnaire reportsForHeadsOfQuestionnaire,
            IAdminAreasReports adminAreasReports)
        {
            _translatorsReports = translatorsReports;

            _girlsReports = girlsReports;

            _loggingReports = loggingReports;

            _reportsForHeadsOfQuestionnaire = reportsForHeadsOfQuestionnaire;

            _adminAreasReports = adminAreasReports;
        }

        public ITranslatorsReports GetTranslatorsReports()
        {
            return _translatorsReports;
        }

        public IGirlsReports GetGirlsReports()
        {
            return _girlsReports;
        }

        public ILoggingReports GetLoggingReports()
        {
            return _loggingReports;
        }

        public IReportsForHeadsOfQuestionnaire GetForHeadsOfQuestionnaireReports()
        {
            return _reportsForHeadsOfQuestionnaire;
        }

        public IAdminAreasReports GetAdminAreasReports()
        {
            return _adminAreasReports;
        }
    }
}
