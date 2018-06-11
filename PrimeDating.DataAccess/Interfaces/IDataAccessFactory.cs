namespace PrimeDating.DataAccess.Interfaces
{
    public interface IDataAccessFactory
    {
        IAdminAreaDataService GetAdminAreaDataService();

        IDictionaryDataService GetDictionaryDataService();

        IGirlsDataService GetGirlsDataService();

        IManagerDataService GetManagerDataService();
    }
}
