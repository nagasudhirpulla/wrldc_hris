using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WrldcHrIs.Application.Departments.Queries.GetDepartments;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Application.Users.Commands.EditUser;
using WrldcHrIs.Application.Users.Queries.GetRawUserById;
using WrldcHrIs.Core.Entities;
using WrldcHrIs.WebApp.Extensions;

namespace WrldcHrIs.WebApp.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditModel(ILogger<EditModel> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public EditUserCommand UpUser { get; set; }

        public SelectList DeptOptions { get; set; }
        public SelectList URoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _mediator.Send(new GetRawUserByIdQuery() { Id = id });
            if (user == null)
            {
                return NotFound();
            }

            UpUser = _mapper.Map<EditUserCommand>(user);

            await InitSelectListItems();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await InitSelectListItems();

            ValidationResult validationCheck = new EditUserCommandValidator().Validate(UpUser);
            validationCheck.AddToModelState(ModelState, nameof(UpUser));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<string> errors = await _mediator.Send(UpUser);

            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            // check if we have any errors and redirect if successful
            if (errors.Count == 0)
            {
                _logger.LogInformation("User edit operation successful");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess("User Editing done");
            }

            return Page();
        }

        public async Task InitSelectListItems()
        {
            DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
            URoles = new SelectList(SecurityConstants.GetRoles());
        }
    }
}