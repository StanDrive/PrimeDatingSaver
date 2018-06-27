namespace ExcelTools.Schematic.Writing
{
    public interface IWbWriterFactory
    {
        IWbWriter Create(string destinationFilePath, WbSchema schema);
    }
}
