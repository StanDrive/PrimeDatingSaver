namespace ExcelTools.Schematic.Reading
{
    public interface IWbReaderFactory
    {
        IWbReader Create(string sourceFilePath);

        ISchemalessReader CreateSchemalessReader(string sourceFilePath);
    }
}
