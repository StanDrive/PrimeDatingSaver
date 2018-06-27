using System;
using System.Globalization;
using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Schematic.CellDataTypes
{
    public class CellDataDate : CellDataBase<DateTime?>
    {
        public CellDataDate(DateTime? value)
            : base(value)
        { }

        public CellDataDate(string rawValue)
            : base(DateTime.FromOADate(int.Parse(rawValue)))
        { }

        internal override CoreCellData ToCoreCellData()
        {
            return new CoreCellDataDate(Value?.ToOADate().ToString(CultureInfo.InvariantCulture));
        }
    }
}