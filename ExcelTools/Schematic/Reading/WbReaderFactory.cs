using Unity;

namespace ExcelTools.Schematic.Reading
{
    internal class WbReaderFactory : IWbReaderFactory
    {
        private readonly IUnityContainer _container;

        public WbReaderFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IWbReader Create(string sourceFilePath)
        {
            return _container.Resolve<IWbReader>(new OrderedParametersOverride(sourceFilePath));
        }

        public ISchemalessReader CreateSchemalessReader(string sourceFilePath)
        {
            return _container.Resolve<ISchemalessReader>(new OrderedParametersOverride(sourceFilePath));
        }
    }
}
