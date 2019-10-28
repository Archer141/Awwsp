namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModels : DbMigration
    {
        public override void Up()
        {
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
                        UserID = c.Int(nullable: false),
                        AgeGroupID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ChildID)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.AgeGroupID)
                .Index(t => t.User_Id);
            
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
                "dbo.Trophies",
                c => new
                    {
                        TrophyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TrophyID);
            
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
            DropForeignKey("dbo.Children", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrophyChilds", "Child_ChildID", "dbo.Children");
            DropForeignKey("dbo.TrophyChilds", "Trophy_TrophyID", "dbo.Trophies");
            DropForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups");
            DropIndex("dbo.TrophyChilds", new[] { "Child_ChildID" });
            DropIndex("dbo.TrophyChilds", new[] { "Trophy_TrophyID" });
            DropIndex("dbo.Children", new[] { "User_Id" });
            DropIndex("dbo.Children", new[] { "AgeGroupID" });
            DropTable("dbo.TrophyChilds");
            DropTable("dbo.Trophies");
            DropTable("dbo.AgeGroups");
            DropTable("dbo.Children");
        }
    }
}
