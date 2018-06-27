using System;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataInt : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.Int;

        public override CellValues DataType => CellValues.Number;

        public CoreCellDataInt(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            throw new NotSupportedException();
        }
    }
}
