using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataString : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.String;

        public override CellValues DataType => CellValues.InlineString;

        public CoreCellDataString(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            return new CellDataString(Value);
        }
    }
}
