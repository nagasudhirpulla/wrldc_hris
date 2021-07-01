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
using WrldcHrIs.Application.CanteenOrders.Commands.EditOrder;
using WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrderById;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.CanteenOrders
{
    public class EditOrderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditOrderModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public EditOrderCommand Inp { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CanteenOrder order = await _mediator.Send(new GetCanteenOrderByIdQuery() { Id = id.Value });

            if (order == null)
            {
                return NotFound();
            }

            Inp = _mapper.Map<EditOrderCommand>(order);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            // validate command
            ValidationResult validationCheck = new EditOrderCommandValidator().Validate(Inp);
            validationCheck.AddToModelState(ModelState, nameof(Inp));

            if (ModelState.IsValid)
            {
                // edit order
                List<string> errors = await _mediator.Send(Inp);

                if (errors.Count == 0)
                {
                    return RedirectToPage("./Index");
                }

                foreach (var err in errors)
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }

            return Page();
        }
    }
}