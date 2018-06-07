using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
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

        protected override void Seed(PrimeDating.DataAccess.PrimeDatingContext context)
        {
            context.HRStatuses.AddOrUpdate(new HRStatuses {Id = 1, Name = "Анкета обработана"},
                new HRStatuses {Id = 2, Name = "Приглашен на собеседование"},
                new HRStatuses {Id = 3, Name = "Прошел собеседование"},
                new HRStatuses {Id = 4, Name = "Приглашен на обучение"},
                new HRStatuses {Id = 5, Name = "Прошел обучение"});

            context.Roles.AddOrUpdate(new Roles {Id = 1, Name = "manager"},
                new Roles {Id = 2, Name = "translator"});
        }
    }
}
