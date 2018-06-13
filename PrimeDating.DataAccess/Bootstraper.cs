using System.Data.Entity;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;
using Unity;

namespace PrimeDating.DataAccess
{
    public static class Bootstraper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IDictionaryDataService, DictionaryDataService>();
            container.RegisterType<IAdminAreaDataService, AdminAreaDataService>();
            container.RegisterType<IGirlsDataService, GirlsDataService>();
            container.RegisterType<IManagerDataService, ManagerDataService>();
            container.RegisterType<IDataAccessFactory, DataAccessFactory>();
            container.RegisterType<IMenDataService, MenDataService>();
            container.RegisterType<IOrdersDataService, OrdersDataService>();
            container.RegisterType<IGiftsDataService, GiftsDataService>();
            container.RegisterType<IPaymentsDataService, PaymentsDataService>();
            container.RegisterType<DbContext, PrimeDatingContext>();

            container.RegisterType<IRepository<Girls>, Repository<Girls>>();
            container.RegisterType<IRepository<Managers>, Repository<Managers>>();
            container.RegisterType<IRepository<AdminAreas>, Repository<AdminAreas>>();
            container.RegisterType<IRepository<GiftOrders>, Repository<GiftOrders>>();
            container.RegisterType<IRepository<Gifts>, Repository<Gifts>>();
        }
    }
}
