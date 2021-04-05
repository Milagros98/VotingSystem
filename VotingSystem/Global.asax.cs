using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VotingSystem.Database;
using VotingSystem.Migrations;
using VotingSystem.Models;

namespace VotingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private DBContext db = new DBContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //this.CheckSuperUser();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckSuperUser()
        {
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            this.CheckRole("Admin", userContext);
            this.CheckRole("User", userContext);

            var user = db.Users.Where(u => u.userName.ToLower().Equals("rruizmcp@gmail.com")).FirstOrDefault();

            if(user == null)
            {
                user = new User
                {
                    userName = "rruizmcp@gmail.com",
                    firtsName = "Milagros",
                    lastName = "Ruiz Patiño",
                    phone = "123434673",
                    address = "Calle 34B #65B -06",
                };

                db.Users.Add(user);
                db.SaveChanges();
          
            }
            // add user to role
            var userASP = userManager.FindByName(user.userName);

            if (userASP == null)
            {
                // create de ASP Net User
                userASP = new ApplicationUser
                {
                    UserName = user.userName,
                    PhoneNumber = user.phone,
                    Email = user.userName,
                };

                userManager.Create(userASP, userASP.UserName);
            }

            userManager.AddToRole(userASP.Id, "Admin");


        }

        private void CheckRole(string roleName, ApplicationDbContext userContext)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Check if the roleName already exists
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }
    }
}
