using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataDecimal : CellDataBase<decimal?>
    {
        public CellDataDecimal(decimal? value)
            : base(value)
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataDecimal(ValueStr);
        }
    }
}
