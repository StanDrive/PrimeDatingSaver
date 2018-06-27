using System;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataDecimal : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.Decimal;

        public override CellValues DataType => CellValues.Number;

        public CoreCellDataDecimal(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            throw new NotSupportedException();
        }
    }
}
