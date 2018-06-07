using PrimeDating.BusinessLayer.Interfaces;
using Unity;

namespace PrimeDating.BusinessLayer
{
    public class Bootstraper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IDailyDataService, DailyDataService>();
            container.RegisterType<ILogger, Nlogger>();
        }
    }
}
