using System;
using System.IO;

namespace PrimeDating.Reports
{
    public interface IGirlsReports
    {
        Stream GirlsReport(DateTime startPeriod, DateTime endPeriod);
    }
}
