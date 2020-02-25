namespace Awwsp.Migrations
{
    using Awwsp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Awwsp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Awwsp.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"&& r.Name == "Parent" && r.Name == "Coach" && r.Name == "HeadCoach"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                var role2 = new IdentityRole { Name = "HeadCoach" };
                var role3 = new IdentityRole { Name = "Coach" };
                var role4 = new IdentityRole { Name = "Parent" };
                
                manager.Create(role);
                manager.Create(role2);
                manager.Create(role3);
                manager.Create(role4);
            }
            if (context.AgeGroups.Count()==0)
            {
                context.AgeGroups.Add(new AgeGroup { Name = "Team U5", MinAge = 3, MaxAge = 5 });
                context.AgeGroups.Add(new AgeGroup { Name = "Team U7", MinAge = 5, MaxAge = 7 });
                context.AgeGroups.Add(new AgeGroup { Name = "Team U9", MinAge = 7, MaxAge = 9 });
                context.AgeGroups.Add(new AgeGroup { Name = "Team U12", MinAge = 9, MaxAge = 12 });
                context.AgeGroups.Add(new AgeGroup { Name = "Team U15", MinAge = 12, MaxAge = 15 });
                context.AgeGroups.Add(new AgeGroup { Name = "Team U18", MinAge = 15, MaxAge = 18 });
            }

            if (!(context.Users.Any(u => u.UserName == "Admin")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@pb.pl", FirstName = "Admin", LastName = "Admin", Email = "admin@pb.pl", EmailConfirmed = true, PhoneNumber = "123456789" };

                manager.Create(user, "Admin!");
                manager.AddToRole(user.Id, "Admin");
            }
            

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
