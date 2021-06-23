using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WrldcHrIs.Core.Entities;
using WrldcHrIs.Infra.Identity;

namespace MeterDataDashboard.Infra.Identity
{
    public class AppDbContextSeed
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public IdentityInit IdentityInit { get; set; }

        /**
         * This method seeds admin, guest role and admin user
         * **/
        public void SeedIdentityData()
        {
            // seed roles
            SeedUserRoles(RoleManager);
            // seed admin user
            SeedUsers(UserManager, IdentityInit);
        }

        /**
         * This method seeds admin and guest users
         * **/
        public void SeedUsers(UserManager<ApplicationUser> userManager, IdentityInit initVariables)
        {
            SeedUser(userManager, initVariables.AdminUserName, initVariables.AdminEmail,
                initVariables.AdminPassword, SecurityConstants.AdminRoleString);
            SeedUser(userManager, initVariables.GuestUserName, initVariables.GuestEmail,
                initVariables.GuestPassword, SecurityConstants.GuestRoleString);
        }

        /**
         * This method seeds a user
         * **/
        public void SeedUser(UserManager<ApplicationUser> userManager, string userName, string email, string password, string role)
        {
            // check if user doesn't exist
            if ((userManager.FindByNameAsync(userName).Result) == null)
            {
                // create desired user object
                ApplicationUser user = new ApplicationUser
                {
                    UserName = userName,
                    Email = email
                };

                // push desired user object to DB
                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    IdentityResult res = userManager.AddToRoleAsync(user, role).Result;
                }
            }
        }

        /**
         * This method seeds roles
         * **/
        public void SeedUserRoles(RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager, SecurityConstants.GuestRoleString);
            SeedRole(roleManager, SecurityConstants.AdminRoleString);
        }

        /**
         * This method seeds a role
         * **/
        public void SeedRole(RoleManager<IdentityRole> roleManager, string roleString)
        {
            // check if role doesn't exist
            if (!(roleManager.RoleExistsAsync(roleString).Result))
            {
                // create desired role object
                IdentityRole role = new IdentityRole
                {
                    Name = roleString,
                };
                // push desired role object to DB
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
