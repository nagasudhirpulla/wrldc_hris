﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                await SeedUserRoles();
                await SeedUsers();
                return true;
            }

            /**
             * This method seeds admin and guest users
             * **/
            public async Task SeedUsers()
            {
                int deptId = (await _context.Departments.Where(d => d.Name.ToLower() == "na").FirstAsync()).Id;
                await SeedUser(_identityInit.AdminUserName, _identityInit.AdminEmail,
                    _identityInit.AdminPassword, SecurityConstants.AdminRoleString, deptId);
                await SeedUser(_identityInit.GuestUserName, _identityInit.GuestEmail,
                    _identityInit.GuestPassword, SecurityConstants.GuestRoleString, deptId);
            }

            /**
             * This method seeds a user
             * **/
            public async Task SeedUser(string userName, string email, string password, string role, int deptId)
            {
                // check if user doesn't exist
                if ((_userManager.FindByNameAsync(userName).Result) == null)
                {
                    // create desired user object
                    ApplicationUser user = new()
                    {
                        UserName = userName,
                        Email = email,
                        DepartmentId = deptId
                    };

                    // push desired user object to DB
                    IdentityResult result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        _ = await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }

            /**
             * This method seeds roles
             * **/
            public async Task SeedUserRoles()
            {
                foreach (string r in SecurityConstants.GetRoles())
                {
                    await SeedRole(r);
                }
                //await SeedRole(SecurityConstants.GuestRoleString);
                //await SeedRole(SecurityConstants.AdminRoleString);
                //await SeedRole(SecurityConstants.EmployeeRoleString);
            }

            /**
             * This method seeds a role
             * **/
            public async Task SeedRole(string roleString)
            {
                // check if role doesn't exist
                if (!(_roleManager.RoleExistsAsync(roleString).Result))
                {
                    // create desired role object
                    IdentityRole role = new IdentityRole
                    {
                        Name = roleString,
                    };
                    // push desired role object to DB
                    _ = await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
