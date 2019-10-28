namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcadamyRoles",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AcadamyRoles");
        }
    }
}
