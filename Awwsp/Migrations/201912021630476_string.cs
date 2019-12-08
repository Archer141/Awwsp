namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "AuthorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "AuthorId", c => c.Int(nullable: false));
        }
    }
}
