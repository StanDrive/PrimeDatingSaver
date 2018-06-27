using System.Collections.Generic;
using System.Linq;

namespace ExcelTools.Schematic
{
    public class WbSchema
    {
        private Dictionary<string, WsSchema> _sheetNameToSchema;
        public string[] WsNames { get; private set; }

        public WbSchema(params WsSchema[] wsSchemas)
        {
            Init(wsSchemas);
        }

        public void Init(WsSchema[] wsSchemas)
        {
            WsNames = wsSchemas.Select(schema => schema.SheetName).ToArray();

            _sheetNameToSchema = wsSchemas.ToDictionary(schema => schema.SheetName);
        }

        public WsSchema GetWsSchema(string sheetName)
        {
            return _sheetNameToSchema[sheetName];
        }
    }
}
