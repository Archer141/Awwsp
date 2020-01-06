namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class istrphy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "IsTrophy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "IsTrophy");
        }
    }
}
