namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notf : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Notfications", newName: "Notifcations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Notifcations", newName: "Notfications");
        }
    }
}
