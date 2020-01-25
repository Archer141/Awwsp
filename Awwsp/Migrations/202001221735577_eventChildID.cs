namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventChildID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "ChildId", c => c.Int());
            CreateIndex("dbo.Notifications", "ChildId");
            AddForeignKey("dbo.Notifications", "ChildId", "dbo.Children", "ChildID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ChildId", "dbo.Children");
            DropIndex("dbo.Notifications", new[] { "ChildId" });
            DropColumn("dbo.Notifications", "ChildId");
        }
    }
}
