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
            container.RegisterType<IAuthenticationData, AuthenticationData>();
            container.RegisterType<IGirlsDataService, GirlsDataService>();
            container.RegisterType<IManagerDataService, ManagerDataService>();
            container.RegisterType<IDataAccessFactory, DataAccessFactory>();
            container.RegisterType<IMenDataService, MenDataService>();
            container.RegisterType<IOrdersDataService, OrdersDataService>();
            container.RegisterType<IGiftsDataService, GiftsDataService>();
            container.RegisterType<IPaymentsDataService, PaymentsDataService>();
            container.RegisterType<IReportsData, ReportsData>();
            container.RegisterType<IDailySaverLogDataService, DailySaverLogDataService>();
            container.RegisterType<DbContext, PrimeDatingContext>();

            container.RegisterType<IRepository<Girls>, Repository<Girls>>();
            container.RegisterType<IRepository<Managers>, Repository<Managers>>();
            container.RegisterType<IRepository<AdminAreas>, Repository<AdminAreas>>();
            container.RegisterType<IRepository<GiftOrders>, Repository<GiftOrders>>();
            container.RegisterType<IRepository<Gifts>, Repository<Gifts>>();
            container.RegisterType<IRepository<GiftStatus>, Repository<GiftStatus>>();
            container.RegisterType<IRepository<GirlsImages>, Repository<GirlsImages>>();
            container.RegisterType<IRepository<GirlsPassportScans>, Repository<GirlsPassportScans>>();
            container.RegisterType<IRepository<HR>, Repository<HR>>();
            container.RegisterType<IRepository<HRStatuses>, Repository<HRStatuses>>();
            container.RegisterType<IRepository<Images>, Repository<Images>>();
            container.RegisterType<IRepository<ManagersGirls>, Repository<ManagersGirls>>();
            container.RegisterType<IRepository<MeetingRequests>, Repository<MeetingRequests>>();
            container.RegisterType<IRepository<MeetingRequestStatuses>, Repository<MeetingRequestStatuses>>();
            container.RegisterType<IRepository<Men>, Repository<Men>>();
            container.RegisterType<IRepository<Orders>, Repository<Orders>>();
            container.RegisterType<IRepository<Payments>, Repository<Payments>>();
            container.RegisterType<IRepository<PaymentTypes>, Repository<PaymentTypes>>();
            container.RegisterType<IRepository<Penalties>, Repository<Penalties>>();
            container.RegisterType<IRepository<Roles>, Repository<Roles>>();
        }
    }
}
