using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WrldcHrIs.Application.Common.Interfaces;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.Application.Users.Commands.SeedUsers
{
    public class SeedUsersCommand : IRequest<bool>
    {
        public class SeedUsersCommandHandler : IRequestHandler<SeedUsersCommand, bool>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IdentityInit _identityInit;
            private readonly IAppDbContext _context;

            public SeedUsersCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityInit identityInit, IAppDbContext context)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _identityInit = identityInit;
                _context = context;
            }

            public async Task<bool> Handle(SeedUsersCommand request, CancellationToken cancellationToken)
            {
                // seed roles
                SeedUserRoles(_roleManager);
                // seed admin user
                SeedUsers(_userManager, _identityInit);
                return true;
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
}
