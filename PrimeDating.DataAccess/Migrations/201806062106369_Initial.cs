namespace PrimeDating.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactsRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminAreaId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        ManId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        ContactsRequestStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.ContactsRequestStatuses", t => t.ContactsRequestStatusId)
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Men", t => t.ManId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.AdminAreaId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId)
                .Index(t => t.ManId)
                .Index(t => t.ContactsRequestStatusId);
            
            CreateTable(
                "dbo.ContactsRequestStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Girls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignedManagerId = c.Int(nullable: false),
                        Passport = c.String(nullable: false, maxLength: 8),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        Patronymic = c.String(maxLength: 100),
                        BirthDay = c.DateTime(nullable: false),
                        ChildrenCount = c.Int(nullable: false),
                        InstagramLink = c.String(maxLength: 250),
                        FacebookLink = c.String(maxLength: 250),
                        VkLink = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 256),
                        City = c.String(nullable: false, maxLength: 100),
                        Country = c.String(nullable: false, maxLength: 100),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        BodyType = c.String(nullable: false, maxLength: 25),
                        MartialStatus = c.String(nullable: false, maxLength: 25),
                        Education = c.String(nullable: false, maxLength: 50),
                        Religion = c.String(nullable: false, maxLength: 30),
                        Smoking = c.String(nullable: false, maxLength: 30),
                        Drinking = c.String(nullable: false, maxLength: 30),
                        ColorEye = c.String(nullable: false, maxLength: 30),
                        ColorHair = c.String(nullable: false, maxLength: 30),
                        LookingFor = c.String(nullable: false, maxLength: 2000),
                        Description = c.String(nullable: false, maxLength: 2000),
                        EngLevel = c.String(nullable: false, maxLength: 30),
                        OtherLangs = c.String(maxLength: 50),
                        WorkPlace = c.String(nullable: false, maxLength: 200),
                        Hobby = c.String(maxLength: 500),
                        AdminAreaId = c.Int(nullable: false),
                        Avatar = c.String(maxLength: 2000),
                        CanReceiveGifts = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.Managers", t => t.AssignedManagerId)
                .Index(t => t.AssignedManagerId)
                .Index(t => t.AdminAreaId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        Patronymic = c.String(maxLength: 100),
                        BirthDay = c.DateTime(),
                        InstagramLink = c.String(maxLength: 250),
                        FacebookLink = c.String(maxLength: 250),
                        VkLink = c.String(maxLength: 250),
                        City = c.String(maxLength: 100),
                        MartialStatus = c.String(maxLength: 25),
                        Skype = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 256),
                        BankCard = c.String(maxLength: 16),
                        Bank = c.String(maxLength: 400),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionary_Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Dictionary_Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Men",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 100),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        Patronymic = c.String(maxLength: 100),
                        BirthDay = c.DateTime(nullable: false),
                        Location = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(maxLength: 20),
                        MartialStatus = c.String(nullable: false, maxLength: 25),
                        Children = c.String(nullable: false, maxLength: 150),
                        Religion = c.String(nullable: false, maxLength: 50),
                        Education = c.String(nullable: false, maxLength: 50),
                        WorkPlace = c.String(nullable: false, maxLength: 100),
                        Drinking = c.String(nullable: false, maxLength: 50),
                        Smoking = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CorrespondenceDailyBalances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminAreaId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        BalanceDate = c.DateTime(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.AdminAreaId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Picture = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiftLink = c.String(nullable: false, maxLength: 2000),
                        GiftStatusUpdateDate = c.DateTime(nullable: false),
                        GiftStatusId = c.Int(nullable: false),
                        AdminAreaId = c.Int(nullable: false),
                        GiftReceivedDate = c.DateTime(nullable: false),
                        ManId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Men", t => t.ManId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .ForeignKey("dbo.Dictionary_GiftStatus", t => t.GiftStatusId)
                .Index(t => t.GiftStatusId)
                .Index(t => t.AdminAreaId)
                .Index(t => t.ManId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Dictionary_GiftStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Girls_Kids",
                c => new
                    {
                        GirlId = c.Int(nullable: false),
                        KidId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.GirlId, t.KidId })
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Kids", t => t.KidId)
                .Index(t => t.GirlId)
                .Index(t => t.KidId);
            
            CreateTable(
                "dbo.Kids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        BirthDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 256),
                        Skype = c.String(maxLength: 100),
                        InfoSource = c.String(nullable: false, maxLength: 200),
                        BirthDay = c.DateTime(nullable: false),
                        LivingArea = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dictionary_HRStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeetingRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminAreaId = c.Int(nullable: false),
                        ManId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        MeetingApprovalDate = c.DateTime(),
                        MeetingRequestStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Men", t => t.ManId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .ForeignKey("dbo.Dictionary_MeetingRequestStatuses", t => t.MeetingRequestStatusId)
                .Index(t => t.AdminAreaId)
                .Index(t => t.ManId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId)
                .Index(t => t.MeetingRequestStatusId);
            
            CreateTable(
                "dbo.Dictionary_MeetingRequestStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Men_Girls",
                c => new
                    {
                        ManId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManId, t.GirlId })
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Men", t => t.ManId)
                .Index(t => t.ManId)
                .Index(t => t.GirlId);
            
            CreateTable(
                "dbo.Penalties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminAreaId = c.Int(nullable: false),
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        PenaltyAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdminAreas", t => t.AdminAreaId)
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.AdminAreaId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Managers_Girls",
                c => new
                    {
                        GirlId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.GirlId, t.ManagerId })
                .ForeignKey("dbo.Girls", t => t.GirlId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.GirlId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Managers_Kids",
                c => new
                    {
                        ManagerId = c.Int(nullable: false),
                        KidId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManagerId, t.KidId })
                .ForeignKey("dbo.Kids", t => t.KidId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.ManagerId)
                .Index(t => t.KidId);
            
            CreateTable(
                "dbo.Managers_Men",
                c => new
                    {
                        ManagerId = c.Int(nullable: false),
                        ManId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManagerId, t.ManId })
                .ForeignKey("dbo.Men", t => t.ManId)
                .ForeignKey("dbo.Managers", t => t.ManagerId)
                .Index(t => t.ManagerId)
                .Index(t => t.ManId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Managers_Men", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers_Men", "ManId", "dbo.Men");
            DropForeignKey("dbo.Managers_Kids", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers_Kids", "KidId", "dbo.Kids");
            DropForeignKey("dbo.Managers_Girls", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers_Girls", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.Penalties", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Penalties", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.Penalties", "AdminAreaId", "dbo.AdminAreas");
            DropForeignKey("dbo.Men_Girls", "ManId", "dbo.Men");
            DropForeignKey("dbo.Men_Girls", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.MeetingRequests", "MeetingRequestStatusId", "dbo.Dictionary_MeetingRequestStatuses");
            DropForeignKey("dbo.MeetingRequests", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.MeetingRequests", "ManId", "dbo.Men");
            DropForeignKey("dbo.MeetingRequests", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.MeetingRequests", "AdminAreaId", "dbo.AdminAreas");
            DropForeignKey("dbo.Girls_Kids", "KidId", "dbo.Kids");
            DropForeignKey("dbo.Girls_Kids", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.Gifts", "GiftStatusId", "dbo.Dictionary_GiftStatus");
            DropForeignKey("dbo.Gifts", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Gifts", "ManId", "dbo.Men");
            DropForeignKey("dbo.Gifts", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.Gifts", "AdminAreaId", "dbo.AdminAreas");
            DropForeignKey("dbo.CorrespondenceDailyBalances", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.CorrespondenceDailyBalances", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.CorrespondenceDailyBalances", "AdminAreaId", "dbo.AdminAreas");
            DropForeignKey("dbo.ContactsRequests", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.ContactsRequests", "ManId", "dbo.Men");
            DropForeignKey("dbo.ContactsRequests", "GirlId", "dbo.Girls");
            DropForeignKey("dbo.Girls", "AssignedManagerId", "dbo.Managers");
            DropForeignKey("dbo.Managers", "RoleId", "dbo.Dictionary_Roles");
            DropForeignKey("dbo.Girls", "AdminAreaId", "dbo.AdminAreas");
            DropForeignKey("dbo.ContactsRequests", "ContactsRequestStatusId", "dbo.ContactsRequestStatuses");
            DropForeignKey("dbo.ContactsRequests", "AdminAreaId", "dbo.AdminAreas");
            DropIndex("dbo.Managers_Men", new[] { "ManId" });
            DropIndex("dbo.Managers_Men", new[] { "ManagerId" });
            DropIndex("dbo.Managers_Kids", new[] { "KidId" });
            DropIndex("dbo.Managers_Kids", new[] { "ManagerId" });
            DropIndex("dbo.Managers_Girls", new[] { "ManagerId" });
            DropIndex("dbo.Managers_Girls", new[] { "GirlId" });
            DropIndex("dbo.Penalties", new[] { "ManagerId" });
            DropIndex("dbo.Penalties", new[] { "GirlId" });
            DropIndex("dbo.Penalties", new[] { "AdminAreaId" });
            DropIndex("dbo.Men_Girls", new[] { "GirlId" });
            DropIndex("dbo.Men_Girls", new[] { "ManId" });
            DropIndex("dbo.MeetingRequests", new[] { "MeetingRequestStatusId" });
            DropIndex("dbo.MeetingRequests", new[] { "ManagerId" });
            DropIndex("dbo.MeetingRequests", new[] { "GirlId" });
            DropIndex("dbo.MeetingRequests", new[] { "ManId" });
            DropIndex("dbo.MeetingRequests", new[] { "AdminAreaId" });
            DropIndex("dbo.Girls_Kids", new[] { "KidId" });
            DropIndex("dbo.Girls_Kids", new[] { "GirlId" });
            DropIndex("dbo.Gifts", new[] { "ManagerId" });
            DropIndex("dbo.Gifts", new[] { "GirlId" });
            DropIndex("dbo.Gifts", new[] { "ManId" });
            DropIndex("dbo.Gifts", new[] { "AdminAreaId" });
            DropIndex("dbo.Gifts", new[] { "GiftStatusId" });
            DropIndex("dbo.CorrespondenceDailyBalances", new[] { "ManagerId" });
            DropIndex("dbo.CorrespondenceDailyBalances", new[] { "GirlId" });
            DropIndex("dbo.CorrespondenceDailyBalances", new[] { "AdminAreaId" });
            DropIndex("dbo.Managers", new[] { "RoleId" });
            DropIndex("dbo.Girls", new[] { "AdminAreaId" });
            DropIndex("dbo.Girls", new[] { "AssignedManagerId" });
            DropIndex("dbo.ContactsRequests", new[] { "ContactsRequestStatusId" });
            DropIndex("dbo.ContactsRequests", new[] { "ManId" });
            DropIndex("dbo.ContactsRequests", new[] { "ManagerId" });
            DropIndex("dbo.ContactsRequests", new[] { "GirlId" });
            DropIndex("dbo.ContactsRequests", new[] { "AdminAreaId" });
            DropTable("dbo.Managers_Men");
            DropTable("dbo.Managers_Kids");
            DropTable("dbo.Managers_Girls");
            DropTable("dbo.Penalties");
            DropTable("dbo.Men_Girls");
            DropTable("dbo.Dictionary_MeetingRequestStatuses");
            DropTable("dbo.MeetingRequests");
            DropTable("dbo.Dictionary_HRStatuses");
            DropTable("dbo.HRs");
            DropTable("dbo.Kids");
            DropTable("dbo.Girls_Kids");
            DropTable("dbo.Dictionary_GiftStatus");
            DropTable("dbo.Gifts");
            DropTable("dbo.Orders");
            DropTable("dbo.CorrespondenceDailyBalances");
            DropTable("dbo.Men");
            DropTable("dbo.Dictionary_Roles");
            DropTable("dbo.Managers");
            DropTable("dbo.Girls");
            DropTable("dbo.ContactsRequestStatuses");
            DropTable("dbo.ContactsRequests");
            DropTable("dbo.AdminAreas");
        }
    }
}
