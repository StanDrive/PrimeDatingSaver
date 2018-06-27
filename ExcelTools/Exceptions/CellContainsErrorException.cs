using System;

namespace ExcelTools.Exceptions
{
    public class CellContainsErrorException : ApplicationException
    {
        public string SheetName { get; private set; }
        public string CellReference { get; private set; }
        public string Value { get; private set; }

        public CellContainsErrorException(string sheetName, string cellReference, string value)
            : base($"Cell contains error. Sheet: {sheetName}, CellReference: {cellReference}, Value: {value}")
        {
            SheetName = sheetName;
            CellReference = cellReference;
            Value = value;
        }
    }
}
