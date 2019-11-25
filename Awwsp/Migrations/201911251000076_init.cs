namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeGroups",
                c => new
                    {
                        AgeGroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgeGroupID);
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        ChildID = c.Int(nullable: false, identity: true),
                        ChildFirstName = c.String(nullable: false),
                        ChildLastName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                        AgeGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChildID)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AgeGroupID);
            
            CreateTable(
                "dbo.Trophies",
                c => new
                    {
                        TrophyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TrophyID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Title = c.String(),
                        AuthorID = c.Int(nullable: false),
                        PhotoID = c.Int(),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.Photos", t => t.PhotoID)
                .Index(t => t.PhotoID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.PhotoID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TrophyChilds",
                c => new
                    {
                        Trophy_TrophyID = c.Int(nullable: false),
                        Child_ChildID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trophy_TrophyID, t.Child_ChildID })
                .ForeignKey("dbo.Trophies", t => t.Trophy_TrophyID, cascadeDelete: true)
                .ForeignKey("dbo.Children", t => t.Child_ChildID, cascadeDelete: true)
                .Index(t => t.Trophy_TrophyID)
                .Index(t => t.Child_ChildID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.News", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Children", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrophyChilds", "Child_ChildID", "dbo.Children");
            DropForeignKey("dbo.TrophyChilds", "Trophy_TrophyID", "dbo.Trophies");
            DropForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups");
            DropIndex("dbo.TrophyChilds", new[] { "Child_ChildID" });
            DropIndex("dbo.TrophyChilds", new[] { "Trophy_TrophyID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.News", new[] { "PhotoID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Children", new[] { "AgeGroupID" });
            DropIndex("dbo.Children", new[] { "UserID" });
            DropTable("dbo.TrophyChilds");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Photos");
            DropTable("dbo.News");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trophies");
            DropTable("dbo.Children");
            DropTable("dbo.AgeGroups");
        }
    }
}
