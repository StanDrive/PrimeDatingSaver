using System;
using System.Collections.Generic;

namespace ExcelTools.Core.Writing
{
    public interface ICoreWbWriter : IDisposable
    {
        void Open();

        void Write(IEnumerable<CoreRowData> rows);
    }
}
