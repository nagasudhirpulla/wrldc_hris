using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrldcHrIs.Application.CanteenOrders.Commands.CreateOrder;
using WrldcHrIs.Application.Common;
using WrldcHrIs.Application.FoodItems.Queries.GetFoodItems;
using WrldcHrIs.Core.Entities;

namespace WrldcHrIs.WebApp.Pages.CanteenOrders
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CreateModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [BindProperty]
        public CreateOrderCommand NewOrder { get; set; }

        public SelectList FoodOpts { get; set; }

        public async Task OnGetAsync()
        {
            string curUsrId = _currentUserService.UserId;
            NewOrder = new() { OrderDate = DateTime.Today, CustomerId = curUsrId };
            await InitSelectListItems();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // validate command
            ValidationResult validationCheck = new CreateOrderCommandValidator().Validate(NewOrder);
            validationCheck.AddToModelState(ModelState, nameof(NewOrder));

            if (ModelState.IsValid)
            {
                // create new order
                List<string> errors = await _mediator.Send(NewOrder);

                if (errors.Count == 0)
                {
                    return RedirectToPage("./Index");
                }

                foreach (var err in errors)
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }

            await InitSelectListItems();
            return Page();
        }


        public async Task InitSelectListItems()
        {
            List<FoodItem> foodItems = await _mediator.Send(new GetFoodItemsQuery());
            foodItems.Insert(0, new FoodItem() { Name = null });
            FoodOpts = new SelectList(foodItems, "Name", "Name", null);
        }

    }
}
