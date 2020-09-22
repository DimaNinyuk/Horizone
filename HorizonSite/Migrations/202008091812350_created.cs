namespace HorizonSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        SNP = c.String(maxLength: 50, unicode: false),
                        Birtday = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.FilmsActor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(),
                        ActorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.ActorId)
                .ForeignKey("dbo.Films", t => t.FilmId)
                .Index(t => t.FilmId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmId = c.Int(nullable: false, identity: true),
                        NameFilm = c.String(maxLength: 100, unicode: false),
                        Duration = c.Int(),
                        DateStart = c.DateTime(storeType: "date"),
                        DateEnd = c.DateTime(storeType: "date"),
                        Company = c.String(maxLength: 50, unicode: false),
                        Picture = c.String(unicode: false),
                        GenreId = c.Int(),
                        IMDBRatffing = c.Decimal(precision: 19, scale: 1),
                        RottenRating = c.Int(),
                        LinkTriller = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.FilmId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.FilmsProducer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilmId = c.Int(),
                        ProdecerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.FilmId)
                .ForeignKey("dbo.Producers", t => t.ProdecerId)
                .Index(t => t.FilmId)
                .Index(t => t.ProdecerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProdecerId = c.Int(nullable: false, identity: true),
                        SNP = c.String(maxLength: 60, unicode: false),
                        Birtday = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ProdecerId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        NameGenre = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50, unicode: false),
                        Date = c.DateTime(storeType: "date"),
                        Text = c.String(unicode: false),
                        FilmId = c.Int(),
                        AdminId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .ForeignKey("dbo.Films", t => t.FilmId)
                .Index(t => t.FilmId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SNP = c.String(maxLength: 50, unicode: false),
                        Date = c.DateTime(storeType: "date"),
                        Phone = c.String(maxLength: 50, unicode: false),
                        Email = c.String(maxLength: 50, unicode: false),
                        Text = c.String(unicode: false),
                        CallTime = c.String(maxLength: 50, unicode: false),
                        AdminId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false),
                        HallId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Time = c.DateTime(),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => new { t.SessionId, t.HallId, t.FilmId })
                .ForeignKey("dbo.Halls", t => t.HallId)
                .ForeignKey("dbo.Films", t => t.FilmId)
                .Index(t => t.HallId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "dbo.Buying",
                c => new
                    {
                        BuyId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(storeType: "date"),
                        SeatId = c.Int(),
                        SessionId = c.Int(),
                        RowId = c.Int(),
                        HallId = c.Int(),
                        FilmId = c.Int(),
                    })
                .PrimaryKey(t => t.BuyId)
                .ForeignKey("dbo.Seats", t => new { t.SeatId, t.RowId })
                .ForeignKey("dbo.Sessions", t => new { t.SessionId, t.HallId, t.FilmId })
                .Index(t => new { t.SeatId, t.RowId })
                .Index(t => new { t.SessionId, t.HallId, t.FilmId });
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatId = c.Int(nullable: false),
                        RowId = c.Int(nullable: false),
                        Number = c.Int(),
                    })
                .PrimaryKey(t => new { t.SeatId, t.RowId })
                .ForeignKey("dbo.Rows", t => t.RowId)
                .Index(t => t.RowId);
            
            CreateTable(
                "dbo.Rows",
                c => new
                    {
                        RowId = c.Int(nullable: false, identity: true),
                        Number = c.Int(),
                        HallId = c.Int(),
                    })
                .PrimaryKey(t => t.RowId)
                .ForeignKey("dbo.Halls", t => t.HallId)
                .Index(t => t.HallId);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        HallId = c.Int(nullable: false, identity: true),
                        NameHall = c.String(maxLength: 20, unicode: false),
                        Information = c.String(maxLength: 50, unicode: false),
                        TypeId = c.Int(),
                        Capacity = c.Int(),
                    })
                .PrimaryKey(t => t.HallId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        NameType = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Sessions", "FilmId", "dbo.Films");
            DropForeignKey("dbo.Buying", new[] { "SessionId", "HallId", "FilmId" }, "dbo.Sessions");
            DropForeignKey("dbo.Seats", "RowId", "dbo.Rows");
            DropForeignKey("dbo.Halls", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Sessions", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Rows", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Buying", new[] { "SeatId", "RowId" }, "dbo.Seats");
            DropForeignKey("dbo.Reviews", "FilmId", "dbo.Films");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Films", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.FilmsProducer", "ProdecerId", "dbo.Producers");
            DropForeignKey("dbo.FilmsProducer", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmsActor", "FilmId", "dbo.Films");
            DropForeignKey("dbo.FilmsActor", "ActorId", "dbo.Actors");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Halls", new[] { "TypeId" });
            DropIndex("dbo.Rows", new[] { "HallId" });
            DropIndex("dbo.Seats", new[] { "RowId" });
            DropIndex("dbo.Buying", new[] { "SessionId", "HallId", "FilmId" });
            DropIndex("dbo.Buying", new[] { "SeatId", "RowId" });
            DropIndex("dbo.Sessions", new[] { "FilmId" });
            DropIndex("dbo.Sessions", new[] { "HallId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "AdminId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reviews", new[] { "AdminId" });
            DropIndex("dbo.Reviews", new[] { "FilmId" });
            DropIndex("dbo.FilmsProducer", new[] { "ProdecerId" });
            DropIndex("dbo.FilmsProducer", new[] { "FilmId" });
            DropIndex("dbo.Films", new[] { "GenreId" });
            DropIndex("dbo.FilmsActor", new[] { "ActorId" });
            DropIndex("dbo.FilmsActor", new[] { "FilmId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Types");
            DropTable("dbo.Halls");
            DropTable("dbo.Rows");
            DropTable("dbo.Seats");
            DropTable("dbo.Buying");
            DropTable("dbo.Sessions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Genres");
            DropTable("dbo.Producers");
            DropTable("dbo.FilmsProducer");
            DropTable("dbo.Films");
            DropTable("dbo.FilmsActor");
            DropTable("dbo.Actors");
        }
    }
}
