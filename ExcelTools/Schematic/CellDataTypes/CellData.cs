using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public abstract class CellData
    {
        public abstract string ValueStr { get; }

        internal abstract CoreCellData ToCoreCellData();
    }
}
