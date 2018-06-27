using System.Collections.Generic;
using System.Linq;
using ExcelTools.Core.CoreCellDataTypes;
using ExcelTools.Schematic;

namespace ExcelTools.Core
{
    public class CoreRowData
    {
        public List<RefAndCell> RefCells { get; private set; }
        public string SheetName { get; private set; }

        public CoreRowData(string sheetName)
        {
            SheetName = sheetName;
            RefCells = new List<RefAndCell>();
        }

        public CoreRowData(string sheetName, params RefAndCell[] refCells)
            : this(sheetName, (IEnumerable<RefAndCell>)refCells)
        { }

        public CoreRowData(string sheetName, IEnumerable<RefAndCell> refCells)
            : this(sheetName)
        {
            RefCells = refCells.ToList();
        }

        public void Add(string columnRef, CoreCellData cell)
        {
            RefCells.Add(new RefAndCell(columnRef, cell));
        }
    }
}
