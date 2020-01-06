namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notfications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        AgeGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupId, cascadeDelete: true)
                .Index(t => t.AgeGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notfications", "AgeGroupId", "dbo.AgeGroups");
            DropIndex("dbo.Notfications", new[] { "AgeGroupId" });
            DropTable("dbo.Notfications");
        }
    }
}
