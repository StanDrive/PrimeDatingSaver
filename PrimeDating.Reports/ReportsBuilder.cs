namespace PrimeDating.Reports
{
    public class ReportsBuilder : IReportsBuilder
    {
        private readonly ITranslatorsReports _translatorsReports;

        private readonly IGirlsReports _girlsReports;

        public ReportsBuilder(ITranslatorsReports translatorsReports, IGirlsReports girlsReports)
        {
            _translatorsReports = translatorsReports;

            _girlsReports = girlsReports;
        }

        public ITranslatorsReports GetTranslatorsReports()
        {
            return _translatorsReports;
        }

        public IGirlsReports GetGirlsReports()
        {
            return _girlsReports;
        }
    }
}
