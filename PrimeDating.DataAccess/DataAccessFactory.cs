using PrimeDating.DataAccess.Interfaces;

namespace PrimeDating.DataAccess
{
    public class DataAccessFactory : IDataAccessFactory
    {
        public IAdminAreaDataService GetAdminAreaDataService()
        {
            return new AdminAreaDataService();
        }

        public IDictionaryDataService GetDictionaryDataService()
        {
            return new DictionaryDataService();
        }

        public IGirlsDataService GetGirlsDataService()
        {
            return new GirlsDataService();
        }

        public IManagerDataService GetManagerDataService()
        {
            return new ManagerDataService();
        }

        public IMenDataService GetMenDataService()
        {
            return new MenDataService();
        }

        public IOrdersDataService GetOrdersDataService()
        {
            return new OrdersDataService();
        }

        public IGiftsDataService GetGiftsDataService()
        {
            return new GiftsDataService();
        }

        public IPaymentsDataService GetPaymentsDataService()
        {
            return new PaymentsDataService();
        }
    }
}
