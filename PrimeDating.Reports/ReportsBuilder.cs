namespace PrimeDating.Reports
{
    public class ReportsBuilder : IReportsBuilder
    {
        private readonly ITranslatorsReports _translatorsReports;

        private readonly IGirlsReports _girlsReports;

        private readonly ILoggingReports _loggingReports;

        public ReportsBuilder(ITranslatorsReports translatorsReports, IGirlsReports girlsReports, ILoggingReports loggingReports)
        {
            _translatorsReports = translatorsReports;

            _girlsReports = girlsReports;

            _loggingReports = loggingReports;
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
    }
}
