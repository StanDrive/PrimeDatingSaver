using System;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataDate : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.Date;

        public override CellValues DataType => CellValues.Date;

        public CoreCellDataDate(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            throw new NotSupportedException();
        }
    }
}
