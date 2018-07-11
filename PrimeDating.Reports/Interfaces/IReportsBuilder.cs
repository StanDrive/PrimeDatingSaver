namespace PrimeDating.Reports.Interfaces
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

        /// <summary>
        /// Gets for heads of questionnaire reports.
        /// </summary>
        /// <returns></returns>
        IReportsForHeadsOfQuestionnaire GetForHeadsOfQuestionnaireReports();
    }
}
