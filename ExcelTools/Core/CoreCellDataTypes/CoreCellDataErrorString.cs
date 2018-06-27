using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataErrorString : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.ErrorString;

        public override CellValues DataType => CellValues.InlineString;

        public CoreCellDataErrorString(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            return new CellDataErrorString(Value);
        }
    }
}
