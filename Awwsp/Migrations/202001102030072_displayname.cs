namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class displayname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trophies", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trophies", "Name", c => c.String());
        }
    }
}
