using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrdersForUserForUser;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.CanteenOrders
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public IndexModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public List<CanteenOrder> Orders { get; set; }
        [BindProperty]
        public GetCanteenOrdersForUserQuery Inp { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _currentUserService.UserId;
            Inp = new GetCanteenOrdersForUserQuery()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                UserId = userId
            };
            Orders = await _mediator.Send(Inp);
        }

        public async Task OnPostAsync()
        {
            var userId = _currentUserService.UserId;
            Inp.UserId = userId;
            Orders = await _mediator.Send(Inp);
        }
    }
}
