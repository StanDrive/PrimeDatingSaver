using System.Data.Entity;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class PrimeDatingContext : DbContext
    {
        public PrimeDatingContext()
            :base("PrimeDatingContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PrimeDatingContext>());
        }

        public DbSet<AdminAreas> AdminAreases { get; set; }

        public DbSet<ContactsRequests> ContactsRequests { get; set; }

        public DbSet<ContactsRequestStatuses> ContactsRequestStatuses { get; set; }

        public DbSet<CorrespondenceDailyBalance> CorrespondenceDailyBalance { get; set; }

        public DbSet<Orders> Gifts { get; set; }

        public DbSet<Gifts> GiftsOrders { get; set; }

        public DbSet<Girls> Girls { get; set; }

        public DbSet<GirlsKids> GirlsKids { get; set; }

        public DbSet<HR> HR { get; set; }

        public DbSet<HRStatuses> HRStatuses { get; set; }

        public DbSet<Kids> Kids { get; set; }

        public DbSet<MeetingRequests> MeetingRequests { get; set; }

        public DbSet<MeetingRequestStatuses> MeetingRequestStatuses { get; set; }

        public DbSet<Men> Men { get; set; }

        public DbSet<MenGirls> MenGirls { get; set; }

        public DbSet<Penalties> Penalties { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Managers> Translators { get; set; }

        public DbSet<ManagersGirls> TranslatorsGirls { get; set; }

        public DbSet<ManagersKids> TranslatorsKids { get; set; }

        public DbSet<ManagersMen> TranslatorsMen { get; set; }
    }
}
