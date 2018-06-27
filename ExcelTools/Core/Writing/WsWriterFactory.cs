using DocumentFormat.OpenXml.Packaging;
using Unity;

namespace ExcelTools.Core.Writing
{
    public class WsWriterFactory : IWsWriterFactory
    {
        private readonly IUnityContainer _container;

        public WsWriterFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IWsWriter Create(WorksheetPart worksheetPart)
        {
            return _container.Resolve<IWsWriter>(new OrderedParametersOverride(worksheetPart));
        }
    }
}
