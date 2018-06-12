using PrimeDating.DataAccess.Interfaces;
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
        }
    }
}
