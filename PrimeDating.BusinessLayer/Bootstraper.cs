using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models.Database;
using Unity;

namespace PrimeDating.BusinessLayer
{
    public class Bootstraper
    {
        public static void Register(IUnityContainer container)
        {
            DataAccess.Bootstraper.Register(container);

            container.RegisterType<IDailyDataService, DailyDataService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<ILogger, Nlogger>();

            container.RegisterType<IEntityService<Logging>, EntityService<Logging>>();
            container.RegisterType<IEntityService<Girls>, EntityService<Girls>>();
            container.RegisterType<IEntityService<Managers>, EntityService<Managers>>();
            container.RegisterType<IEntityService<AdminAreas>, EntityService<AdminAreas>>();
            container.RegisterType<IEntityService<GiftOrders>, EntityService<GiftOrders>>();
            container.RegisterType<IEntityService<Gifts>, EntityService<Gifts>>();
            container.RegisterType<IEntityService<GiftStatus>, EntityService<GiftStatus>>();
            container.RegisterType<IEntityService<GirlsImages>, EntityService<GirlsImages>>();
            container.RegisterType<IEntityService<GirlsPassportScans>, EntityService<GirlsPassportScans>>();
            container.RegisterType<IEntityService<HR>, EntityService<HR>>();
            container.RegisterType<IEntityService<HRStatuses>, EntityService<HRStatuses>>();
            container.RegisterType<IEntityService<Images>, EntityService<Images>>();
            container.RegisterType<IEntityService<ManagersGirls>, EntityService<ManagersGirls>>();
            container.RegisterType<IEntityService<MeetingRequests>, EntityService<MeetingRequests>>();
            container.RegisterType<IEntityService<MeetingRequestStatuses>, EntityService<MeetingRequestStatuses>>();
            container.RegisterType<IEntityService<Men>, EntityService<Men>>();
            container.RegisterType<IEntityService<Orders>, EntityService<Orders>>();
            container.RegisterType<IEntityService<Payments>, EntityService<Payments>>();
            container.RegisterType<IEntityService<PaymentTypes>, EntityService<PaymentTypes>>();
            container.RegisterType<IEntityService<Penalties>, EntityService<Penalties>>();
            container.RegisterType<IEntityService<Roles>, EntityService<Roles>>();
        }
    }
}
