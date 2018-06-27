using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataErrorString : CellDataBase<string>
    {
        public CellDataErrorString(string value)
            : base(value)
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataErrorString(Value);
        }
    }
}