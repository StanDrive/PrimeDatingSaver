using ExcelTools.Core.Reading;
using ExcelTools.Core.Writing;
using ExcelTools.Schematic.Reading;
using ExcelTools.Schematic.Writing;
using Unity;

namespace ExcelTools
{
    public static class DiBootstrapper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IWbReader, WbReader>();
            container.RegisterType<IWbWriter, WbWriter>();

            container.RegisterType<IWbReaderFactory, WbReaderFactory>();
            container.RegisterType<IWbWriterFactory, WbWriterFactory>();

            container.RegisterType<IWsWriter, AsyncWsWriter>();
            container.RegisterType<IWsWriterFactory, WsWriterFactory>();

            container.RegisterType<ICoreWbReader, CoreWbReader>();
            container.RegisterType<ICoreWbReaderFactory, CoreWbReaderFactory>();

            container.RegisterType<ICoreWbWriter, CoreWbWriter>();
            container.RegisterType<ICoreWbFactory, CoreWbWriterFactory>();

            container.RegisterType<ISchemalessReader, SchemalessReader>();
        }
    }
}
