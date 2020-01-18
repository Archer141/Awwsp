namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Color = c.String(),
                        TextColor = c.String(),
                        AllDay = c.Boolean(nullable: false),
                        AgeGroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupID, cascadeDelete: true)
                .Index(t => t.AgeGroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "AgeGroupID", "dbo.AgeGroups");
            DropIndex("dbo.Events", new[] { "AgeGroupID" });
            DropTable("dbo.Events");
        }
    }
}
