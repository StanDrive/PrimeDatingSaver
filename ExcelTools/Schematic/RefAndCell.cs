using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic
{
    public class RefAndCell
    {
        public string ColumnRef { get; private set; }
        public CoreCellData Cell { get; private set; }

        public RefAndCell(string columnRef, CoreCellData cell)
        {
            ColumnRef = columnRef;
            Cell = cell;
        }
    }
}
