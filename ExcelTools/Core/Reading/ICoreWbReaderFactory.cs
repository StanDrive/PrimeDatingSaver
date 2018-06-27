namespace ExcelTools.Core.Reading
{
    public interface ICoreWbReaderFactory
    {
        ICoreWbReader Create(string filePath, string wsName);
    }
}
