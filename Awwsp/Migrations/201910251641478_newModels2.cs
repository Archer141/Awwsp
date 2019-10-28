namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModels2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Title = c.String(),
                        AuthorID = c.Int(nullable: false),
                        PhotoID = c.Int(),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.Photos", t => t.PhotoID)
                .Index(t => t.PhotoID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Localization = c.String(),
                    })
                .PrimaryKey(t => t.PhotoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "PhotoID", "dbo.Photos");
            DropIndex("dbo.News", new[] { "PhotoID" });
            DropTable("dbo.Photos");
            DropTable("dbo.News");
        }
    }
}
