using Unity;

namespace ExcelTools.Core.Reading
{
    internal class CoreWbReaderFactory : ICoreWbReaderFactory
    {
        private readonly IUnityContainer _container;

        public CoreWbReaderFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ICoreWbReader Create(string filePath, string wsName)
        {
            return _container.Resolve<ICoreWbReader>(new OrderedParametersOverride(filePath, wsName));
        }
    }
}
