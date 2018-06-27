using Unity;

namespace ExcelTools.Core.Writing
{
    internal class CoreWbWriterFactory : ICoreWbFactory
    {
        private readonly IUnityContainer _container;

        public CoreWbWriterFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ICoreWbWriter Create(string filePath, params string[] sheetNames)
        {
            return _container.Resolve<ICoreWbWriter>(new OrderedParametersOverride(filePath, sheetNames));
        }
    }
}