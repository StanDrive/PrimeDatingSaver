using System;
using System.Collections.Generic;

namespace ExcelTools.Schematic.Writing
{
    public interface IWbWriter : IDisposable
    {
        void Open();

        void Write(IEnumerable<RowData> rows);

        void WriteSuccessivelyNotSchematicData(IEnumerable<RowData> rows);
    }
}
