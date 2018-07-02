using System.Data;
using System.IO;

namespace PrimeDating.Reports
{
    public interface ISpreadsheetBuilder
    {
        Stream GetSpreadsheetFromDataTable(DataTable table);
    }
}
