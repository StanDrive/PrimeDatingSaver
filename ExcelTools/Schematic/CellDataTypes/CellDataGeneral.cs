using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataGeneral : CellDataBase<string>
    {
        public CellDataGeneral(string rawValue)
            : base(rawValue)
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataGeneral(Value);
        }
    }
}