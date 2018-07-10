﻿using Unity;

namespace PrimeDating.Reports
{
    public static class Bootstraper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<ITranslatorsReports, TranslatorsReports>();
            container.RegisterType<IGirlsReports, GirlsReports>();
            container.RegisterType<IReportsBuilder, ReportsBuilder>();
            container.RegisterType<ILoggingReports, LoggingReports>();
            container.RegisterType<IReportsForHeadsOfQuestionnaire, ReportsForHeadsOfQuestionnaire>();
            container.RegisterType<ISpreadsheetBuilder, SpreadsheetBuilder>();
        }
    }
}
