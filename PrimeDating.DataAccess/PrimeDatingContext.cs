using System.Data.Entity;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class PrimeDatingContext : DbContext
    {
        public PrimeDatingContext()
            :base("PrimeDatingContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PrimeDatingContext>());
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<AdminAreas> AdminAreases { get; set; }

        public DbSet<ContactsRequests> ContactsRequests { get; set; }

        public DbSet<GirlsImages> GirlsImages { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Images> Images { get; set; }

        public DbSet<ContactsRequestStatuses> ContactsRequestStatuses { get; set; }

        public DbSet<Gifts> Gifts { get; set; }

        public DbSet<GiftStatus> GiftStatuses { get; set; }

        public DbSet<GirlsPassportScans> GirlsPassportScans { get; set; }

        public DbSet<GiftOrders> GiftOrders { get; set; }

        public DbSet<Payments> Payments { get; set; }

        public DbSet<PaymentTypes> PaymentTypes { get; set; }

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

        public DbSet<Managers> Managers { get; set; }

        public DbSet<ManagersGirls> ManagersGirls { get; set; }

        public DbSet<ManagersKids> ManagersKids { get; set; }

        public DbSet<ManagersMen> ManagersMen { get; set; }

        public DbSet<Logging> Logging { get; set; }

        public DbSet<DailyDataSaverLog> DailyDataSaverLog { get; set; }
    }
}
