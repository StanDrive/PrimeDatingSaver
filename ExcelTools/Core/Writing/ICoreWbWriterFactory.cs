namespace ExcelTools.Core.Writing
{
    public interface ICoreWbFactory
    {
        ICoreWbWriter Create(string filePath, params string[] sheetNames);
    }
}
