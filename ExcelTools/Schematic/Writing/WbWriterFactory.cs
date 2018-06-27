using Unity;

namespace ExcelTools.Schematic.Writing
{
    internal class WbWriterFactory : IWbWriterFactory
    {
        private readonly IUnityContainer _container;

        public WbWriterFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IWbWriter Create(string destinationFilePath, WbSchema schema)
        {
            return _container.Resolve<IWbWriter>(new OrderedParametersOverride(destinationFilePath, schema));
        }
    }
}
