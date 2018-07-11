using System.Data;
using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface ISpreadsheetBuilder
    {
        /// <summary>
        /// Gets the spreadsheet from data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        Stream GetSpreadsheetFromDataTable(DataTable table);
    }
}
