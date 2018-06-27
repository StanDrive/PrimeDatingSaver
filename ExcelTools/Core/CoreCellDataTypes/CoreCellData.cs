using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public abstract class CoreCellData
    {
        public string Value { get; private set; }

        public abstract CellFormatIndex Style { get; }
        public abstract CellValues DataType { get; }

        protected CoreCellData(string value)
        {
            Value = value;
        }

        internal abstract CellData ToCellData();
    }
}