using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataInt : CellDataBase<int?>
    {
        public CellDataInt(int? value)
            : base(value)
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataInt(ValueStr);
        }
    }
}
