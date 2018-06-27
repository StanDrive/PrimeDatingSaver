using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataString : CellDataBase<string>
    {
        public CellDataString(string rawValue)
            : base(rawValue)
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataString(Value);
        }
    }
}