using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PrimeDatingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            var migrator = new DbMigrator(this);

            var pendingMigrations = migrator.GetPendingMigrations().Any();

            if (!pendingMigrations)
            {
                return;
            }
            migrator.Update();

            Seed(new PrimeDatingContext());
        }

        protected override void Seed(PrimeDatingContext context)
        {
            context.HRStatuses.AddOrUpdate(new HRStatuses {Id = 1, Name = "Анкета обработана"},
                new HRStatuses {Id = 2, Name = "Приглашен на собеседование"},
                new HRStatuses {Id = 3, Name = "Прошел собеседование"},
                new HRStatuses {Id = 4, Name = "Приглашен на обучение"},
                new HRStatuses {Id = 5, Name = "Прошел обучение"});

            context.Roles.AddOrUpdate(new Roles {Id = 1, Name = "manager"},
                new Roles {Id = 2, Name = "translator"});

            context.GiftStatuses.AddOrUpdate(new GiftStatus{ Id = 1, Name = "received" });

            context.PaymentTypes.AddOrUpdate(new PaymentTypes {Id = 1, Name = "chat_message"},
                new PaymentTypes {Id = 2, Name = "chat_photo"},
                new PaymentTypes {Id = 3, Name = "chat_comment"},
                new PaymentTypes {Id = 4, Name = "chat_wink"},
                new PaymentTypes {Id = 5, Name = "chat_gift"},
                new PaymentTypes {Id = 6, Name = "chat_sticker"},
                new PaymentTypes {Id = 7, Name = "chat_sticker"},
                new PaymentTypes {Id = 8, Name = "correspondence_female_send"},
                new PaymentTypes {Id = 9, Name = "correspondence_watch_attachment"},
                new PaymentTypes {Id = 10, Name = "real_gift_delivery_approved"},
                new PaymentTypes {Id = 11, Name = "real_gift_delivery_failure"},
                new PaymentTypes {Id = 12, Name = "video_stream"},
                new PaymentTypes {Id = 13, Name = "video_intro"},
                new PaymentTypes {Id = 14, Name = "mail_attachment_video"},
                new PaymentTypes {Id = 15, Name = "acs_moderator_credits"},
                new PaymentTypes {Id = 16, Name = "meeting_complete"},
                new PaymentTypes {Id = 17, Name = "competition_vote"},
                new PaymentTypes {Id = 18, Name = "request_contact_approved"});
        }
    }
}
