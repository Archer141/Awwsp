namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _null : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups");
            DropIndex("dbo.Children", new[] { "AgeGroupID" });
            AlterColumn("dbo.Children", "AgeGroupID", c => c.Int());
            CreateIndex("dbo.Children", "AgeGroupID");
            AddForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups", "AgeGroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups");
            DropIndex("dbo.Children", new[] { "AgeGroupID" });
            AlterColumn("dbo.Children", "AgeGroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Children", "AgeGroupID");
            AddForeignKey("dbo.Children", "AgeGroupID", "dbo.AgeGroups", "AgeGroupID", cascadeDelete: true);
        }
    }
}
