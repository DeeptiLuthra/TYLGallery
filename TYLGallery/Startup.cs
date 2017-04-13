using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TYLGallery.Models;
using TYLGallery.Common;

[assembly: OwinStartup(typeof(TYLGallery.Startup))]

namespace TYLGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists(ApplicationConstants.Keys.AdminRole))
            {

                // first we create Admin rool   
                var role = new IdentityRole()
                {
                    Name = ApplicationConstants.Keys.AdminRole
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = user.Email = "admin@tylgallery.com";

                string userPWD = "TyL@g@!!erY";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }

            //We're not creating users yet.
            //if (!roleManager.RoleExists("User"))
            //{
            //    var role = new IdentityRole {Name = "User"};
            //    roleManager.Create(role);
            //}
        }
    }
}
