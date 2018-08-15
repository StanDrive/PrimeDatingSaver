namespace PrimeDating.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DailyDataLogging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyDataSaverLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Message = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyDataSaverLogs");
        }
    }
}
