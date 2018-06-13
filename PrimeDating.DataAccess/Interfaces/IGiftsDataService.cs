using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IGiftsDataService
    {
        void AddOrUpdateGifts(List<Gifts> gifts);

        void AddOrUpdateGiftOrders(List<GiftOrders> giftOrders);
    }
}
