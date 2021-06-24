using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WrldcHrIs.Application.Departments.Queries.GetDepartments;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Application.Users.Commands.CreateUser;
using WrldcHrIs.WebApp.Extensions;

namespace WrldcHrIs.WebApp.Pages.Users
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class CreateModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        //https://www.learnrazorpages.com/razor-pages/forms/select-lists
        public SelectList DeptOptions { get; set; }
        
        [BindProperty]
        public CreateUserCommand NewUser { get; set; }
        public CreateModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task OnGetAsync()
        {
            DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");

            IdentityResult result = await _mediator.Send(NewUser);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Created new account for {NewUser.Username}");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess($"Created new user {NewUser.Username}");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();

        }
    }
}