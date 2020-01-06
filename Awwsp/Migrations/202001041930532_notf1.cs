namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notf1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Notifcations", newName: "Notifications");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Notifications", newName: "Notifcations");
        }
    }
}
