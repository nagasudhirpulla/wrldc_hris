using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrders;
using WrldcHrIs.Application.Users;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.CanteenOrders
{
    [Authorize(Roles = SecurityConstants.AdminRoleString + "," + SecurityConstants.CanteenMgrRoleString)]
    public class ViewOrdersModel : PageModel
    {
        private readonly IMediator _mediator;

        public ViewOrdersModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<CanteenOrder> Orders { get; set; }
        [BindProperty]
        public GetCanteenOrdersQuery Inp { get; set; }

        public async Task OnGetAsync()
        {
            Inp = new GetCanteenOrdersQuery()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            Orders = await _mediator.Send(Inp);
        }

        public async Task OnPostAsync()
        {
            Orders = await _mediator.Send(Inp);
        }
    }
}
