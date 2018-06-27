using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelTools.Schematic
{
    public class WsSchema
    {
        private readonly Dictionary<string, string> _columnRefToColumnName;
        private readonly Dictionary<string, string> _columnNameToColumnRef;

        public string SheetName { get; private set; }
        public string[] ColumnNames { get; private set; }

        public WsSchema(string sheetName, params string[] columnNames)
            : this(sheetName, BuildSimpleColumnMap(columnNames))
        { }

        public WsSchema(string sheetName, params ColumnNameAndRef[] maps)
        {
            SheetName = sheetName;

            ColumnNames = maps.Select(map => map.ColumnName).ToArray();

            _columnRefToColumnName = new Dictionary<string, string>();
            _columnNameToColumnRef = new Dictionary<string, string>();

            foreach (ColumnNameAndRef map in maps)
            {
                _columnNameToColumnRef[map.ColumnName] = map.ColumnRef;
                _columnRefToColumnName[map.ColumnRef] = map.ColumnName;
            }
        }

        public string GetColumnNameFromRef(string columnRef)
        {
            try
            {
                return _columnRefToColumnName[columnRef];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException($"Unknown field in column {columnRef}");
            }
        }

        public string GetColumnRefFromName(string columnName)
        {
            return _columnNameToColumnRef[columnName];
        }

        private static ColumnNameAndRef[] BuildSimpleColumnMap(string[] columnNames)
        {
            return
                columnNames.Select((t, i) => new ColumnNameAndRef(t, ConvertColumnNumberToColumnRef(i + 1))).ToArray();
        }

        public static string ConvertColumnNumberToColumnRef(int columnNumber)
        {
            var columnString = string.Empty;
            decimal number = columnNumber;

            while (number > 0)
            {
                var currentLetterNumber = (number - 1) % 26;
                var currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                number = (number - (currentLetterNumber + 1)) / 26;
            }

            return columnString;
        }
    }
}
