using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Application.Users.Queries.GetAppUsers;
using WrldcHrIs.Application.Users.Queries.GetUserById;

namespace WrldcHrIs.WebApp.Pages.Users
{
    public class MyDetailsModel : PageModel
    {

        private readonly ILogger<MyDetailsModel> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public MyDetailsModel(ILogger<MyDetailsModel> logger, IMediator mediator, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        public UserDTO CUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CUser = await _mediator.Send(new GetUserByIdQuery() { Id = _currentUserService.UserId });
            if (CUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
