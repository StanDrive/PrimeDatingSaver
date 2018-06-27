using System;

namespace ExcelTools.Schematic.Reading
{
    public class TargetSheetHeadingRowIsInvalidException : Exception
    {
        public string TargetSheetName { get; private set; }
        public string MissingColumn { get; private set; }

        public TargetSheetHeadingRowIsInvalidException(string targetSheetName, string missingColumn)
            : base(
                $"Sheet: '{targetSheetName}' heading row doesn't contain expected column. ColumnName: {missingColumn}")
        {
            TargetSheetName = targetSheetName;
            MissingColumn = missingColumn;
        }
    }
}
