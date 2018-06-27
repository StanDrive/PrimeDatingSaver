namespace ExcelTools.Schematic
{
    public class ColumnNameAndRef
    {
        public string ColumnName { get; private set; }
        public string ColumnRef { get; private set; }

        public ColumnNameAndRef(string columnName, string columnRef)
        {
            ColumnName = columnName;
            ColumnRef = columnRef;
        }
    }
}
