using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WrldcHrIs.Application.CanteenOrders.Commands.DeleteOrder;
using WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrderById;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.CanteenOrders
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CanteenOrder Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _mediator.Send(new GetCanteenOrderByIdQuery() { Id = id.Value });

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeleteOrderCommand delCommand = new() { OrderId = id.Value };

            // validate command
            ValidationResult validationCheck = new DeleteOrderCommandValidator().Validate(delCommand);

            if (!validationCheck.IsValid)
            {
                return BadRequest();
            }

            _ = await _mediator.Send(delCommand);

            return RedirectToPage("./Index");
        }

    }
}
