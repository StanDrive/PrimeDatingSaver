﻿namespace PrimeDating.DataAccess.Interfaces
{
    public interface IDataAccessFactory
    {
        IAdminAreaDataService GetAdminAreaDataService();

        IDictionaryDataService GetDictionaryDataService();

        IGirlsDataService GetGirlsDataService();

        IManagerDataService GetManagerDataService();

        IMenDataService GetMenDataService();

        IOrdersDataService GetOrdersDataService();

        IGiftsDataService GetGiftsDataService();

        IPaymentsDataService GetPaymentsDataService();
    }
}
