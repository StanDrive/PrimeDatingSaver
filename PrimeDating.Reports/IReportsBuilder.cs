namespace PrimeDating.Reports
{
    public interface IReportsBuilder
    {
        /// <summary>
        /// Gets the translators reports.
        /// </summary>
        /// <returns></returns>
        ITranslatorsReports GetTranslatorsReports();

        /// <summary>
        /// Gets the girls reports.
        /// </summary>
        /// <returns></returns>
        IGirlsReports GetGirlsReports();

        /// <summary>
        /// Gets the logging reports.
        /// </summary>
        /// <returns></returns>
        ILoggingReports GetLoggingReports();
    }
}
