using DocumentFormat.OpenXml.Packaging;

namespace ExcelTools.Core.Writing
{
    public interface IWsWriterFactory
    {
        IWsWriter Create(WorksheetPart worksheetPart);
    }
}
