using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class GiftsDataService : IGiftsDataService
    {
        public void AddOrUpdateGifts(List<Gifts> gifts)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var gift in gifts)
                {
                    context.Gifts.AddOrUpdate(gift);
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdateGiftOrders(List<GiftOrders> giftOrders)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var giftOrder in giftOrders)
                {
                    context.GiftOrders.AddOrUpdate(giftOrder);
                }

                context.SaveChanges();
            }
        }
    }
}
