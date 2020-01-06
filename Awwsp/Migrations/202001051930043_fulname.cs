namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fulname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Children", "FullName");
        }
    }
}
