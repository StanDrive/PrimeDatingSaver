namespace ExcelTools.Schematic.Reading
{
    public interface ISchemalessReader
    {
        /// <summary>
        /// Opens the specified sheet name. If omitted, then will take the first one.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        void Open(string sheetName);

        /// <summary>
        /// Skips the specified count of rows.
        /// Empty rows didn't are not taken into account.
        /// </summary>
        /// <param name="count">The count.</param>
        void Skip(int count);

        /// <summary>
        /// Reads this worksheet and return json result.
        /// </summary>
        /// <returns>System.String.</returns>
        string Read();

        /// <summary>
        /// Extracts parts of a spreadsheet rows and returns the extracted parts in a new json.
        /// </summary>
        /// <param name="begin">The position where to begin the extraction. First character is at position 0.</param>
        /// <param name="end">The position (up to, but not including) where to end the extraction.</param>
        /// <returns>System.String.</returns>
        string Slice(int begin, int end);

        /// <summary>
        /// Extracts parts of a spreadsheet rows and returns the extracted parts in a new json.
        /// Selects all rows from the start-position to the end of the worksheet.
        /// </summary>
        /// <param name="begin">The position where to begin the extraction. First character is at position 0.</param>
        /// <returns>System.String.</returns>
        string Slice(int begin);
    }
}
