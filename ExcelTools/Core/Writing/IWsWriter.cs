using System;

namespace ExcelTools.Core.Writing
{
    public interface IWsWriter : IDisposable
    {
        void WriteRow(CoreRowData row);
    }
}
