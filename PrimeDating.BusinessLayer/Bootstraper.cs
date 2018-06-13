using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models.Database;
using Unity;

namespace PrimeDating.BusinessLayer
{
    public class Bootstraper
    {
        public static void Register(IUnityContainer container)
        {
            PrimeDating.DataAccess.Bootstraper.Register(container);

            container.RegisterType<IDailyDataService, DailyDataService>();
            container.RegisterType<ILogger, Nlogger>();

            container.RegisterType<IEntityService<Girls>, EntityService<Girls>>();
            container.RegisterType<IEntityService<Managers>, EntityService<Managers>>();
            container.RegisterType<IEntityService<AdminAreas>, EntityService<AdminAreas>>();
            container.RegisterType<IEntityService<GiftOrders>, EntityService<GiftOrders>>();
            container.RegisterType<IEntityService<Gifts>, EntityService<Gifts>>();
        }
    }
}
