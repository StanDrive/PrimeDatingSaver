using System;

namespace ExcelTools.Core.Reading
{
    public interface ICoreWbReader : IDisposable
    {
        void Open();

        bool Read();

        CoreRowData Current { get; }
    }
}
