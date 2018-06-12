using System.Collections.Generic;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IGiftsDataService
    {
        void AddOrUpdateGifts(List<Gifts> gifts);

        void AddOrUpdateGiftOrders(List<GiftOrders> giftOrders);
    }
}
