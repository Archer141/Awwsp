namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Perceived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Perceived");
        }
    }
}
