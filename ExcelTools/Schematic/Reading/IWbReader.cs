using System;

namespace ExcelTools.Schematic.Reading
{
    public interface IWbReader : IDisposable
    {
        RowData Current { get; }

        void Open(WbSchema wbSchema);

        bool Read();

        bool Skip(int count);

        void SkipHeader(WbSchema schema);

        bool Read(WbSchema schema);
    }
}
