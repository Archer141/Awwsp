namespace Awwsp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Awwsp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Awwsp.Models.ApplicationDbContext";
        }

        protected override void Seed(Awwsp.Models.ApplicationDbContext context)
        {
            
            //context.Roles.AddOrUpdate(
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Admin"),
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Parent"),
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("HeadCoach"),
            //    new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Coach")
            //    );

            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
