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
    }
}
