namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trophies", "PhotoID", c => c.Int());
            AddColumn("dbo.Trophies", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Photos", "Date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Trophies", "PhotoID");
            AddForeignKey("dbo.Trophies", "PhotoID", "dbo.Photos", "PhotoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trophies", "PhotoID", "dbo.Photos");
            DropIndex("dbo.Trophies", new[] { "PhotoID" });
            DropColumn("dbo.Photos", "Date");
            DropColumn("dbo.News", "Date");
            DropColumn("dbo.Trophies", "Date");
            DropColumn("dbo.Trophies", "PhotoID");
        }
    }
}
