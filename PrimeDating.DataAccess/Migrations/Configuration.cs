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
                new PaymentTypes {Id = 2, Name = "chat_photo", Penalty = 0},
                new PaymentTypes {Id = 3, Name = "chat_comment", Penalty = 0},
                new PaymentTypes {Id = 4, Name = "chat_wink", Penalty = 0},
                new PaymentTypes {Id = 5, Name = "chat_gift", Penalty = 0},
                new PaymentTypes {Id = 6, Name = "chat_sticker", Penalty = 0},
                new PaymentTypes {Id = 7, Name = "correspondence_female_send", Penalty = 0},
                new PaymentTypes {Id = 8, Name = "correspondence_watch_attachment", Penalty = 0},
                new PaymentTypes {Id = 9, Name = "real_gift_delivery_approved", Penalty = 0},
                new PaymentTypes {Id = 10, Name = "real_gift_delivery_failure", Penalty = 1},
                new PaymentTypes {Id = 11, Name = "video_stream", Penalty = 0},
                new PaymentTypes {Id = 12, Name = "video_intro", Penalty = 0},
                new PaymentTypes {Id = 13, Name = "mail_attachment_video", Penalty = 0},
                new PaymentTypes {Id = 14, Name = "acs_moderator_credits", Penalty = 0},
                new PaymentTypes {Id = 15, Name = "meeting_complete", Penalty = 0},
                new PaymentTypes {Id = 16, Name = "competition_vote", Penalty = 0},
                new PaymentTypes {Id = 17, Name = "request_contact_approved", Penalty = 0});

            context.SaveChanges();
        }
    }
}
