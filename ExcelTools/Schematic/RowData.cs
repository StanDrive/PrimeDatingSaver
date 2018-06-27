using System.Collections;
using System.Collections.Generic;
using ExcelTools.Schematic.CellDataTypes;

namespace ExcelTools.Schematic
{
    public class RowData : IEnumerable<KeyValuePair<string, CellData>>
    {
        private readonly Dictionary<string, CellData> _cells;
        public string SheetName { get; private set; }

        public RowData(string sheetName)
        {
            SheetName = sheetName;
            _cells = new Dictionary<string, CellData>();
        }

        public RowData(string sheetName, Dictionary<string, CellData> cells)
            : this(sheetName)
        {
            _cells = cells;
        }

        public CellData this[string columnName]
        {
            get
            {
                CellData cell;

                return _cells.TryGetValue(columnName, out cell)
                    ? cell
                    : null;
            }
        }

        public void Add(string columnName, CellData cell)
        {
            _cells.Add(columnName, cell);
        }

        public void Remove(string columnName)
        {
            _cells.Remove(columnName);
        }

        public IEnumerator<KeyValuePair<string, CellData>> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }
    }
}
