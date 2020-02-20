namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _bool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "IsSignOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Children", "IsSignOut");
        }
    }
}
