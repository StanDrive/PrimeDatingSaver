using System;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Core.CoreCellDataTypes
{
    public class CoreCellDataGeneral : CoreCellData
    {
        public override CellFormatIndex Style => CellFormatIndex.General;

        public override CellValues DataType => CellValues.InlineString;

        public CoreCellDataGeneral(string value)
            : base(value)
        { }

        internal override CellData ToCellData()
        {
            throw new NotSupportedException();
        }
    }
}
