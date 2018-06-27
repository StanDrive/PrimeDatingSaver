using System;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataHeaderString : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.HeaderString;

        public override CellValues DataType => CellValues.InlineString;

        public CoreCellDataHeaderString(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            throw new NotSupportedException();
        }
    }
}
