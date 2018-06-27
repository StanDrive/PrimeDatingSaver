using System;

namespace ExcelTools.Exceptions
{
    public class TargetSheetDoesNotExistException : ApplicationException
    {
        public string TargetSheetName { get; private set; }

        public TargetSheetDoesNotExistException(string targetSheetName)
            : base($"Target sheet doesn't exist. SheetName: {targetSheetName}")
        {
            TargetSheetName = targetSheetName;
        }
    }
}
