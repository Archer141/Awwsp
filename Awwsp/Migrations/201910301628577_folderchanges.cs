namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class folderchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "Image");
        }
    }
}
