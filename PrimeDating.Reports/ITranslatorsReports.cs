using System;
using System.IO;

namespace PrimeDating.Reports
{
    public interface ITranslatorsReports
    {
        Stream TranslatorsReport(DateTime startPeriod, DateTime endPeriod);
    }
}
