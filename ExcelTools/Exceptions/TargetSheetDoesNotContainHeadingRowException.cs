using System;

namespace ExcelTools.Exceptions
{
    public class TargetSheetDoesNotContainHeadingRowException : ApplicationException
    {
        public string TargetSheetName { get; private set; }

        public TargetSheetDoesNotContainHeadingRowException(string sheetName)
            : base($"No heading row. SheetName: {sheetName}")
        {
            TargetSheetName = sheetName;
        }
    }
}
