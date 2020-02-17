namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requnews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Title", c => c.String());
            AlterColumn("dbo.News", "Text", c => c.String());
        }
    }
}
