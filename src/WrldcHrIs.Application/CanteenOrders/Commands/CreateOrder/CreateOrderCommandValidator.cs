using FluentValidation;
using System;

namespace WrldcHrIs.Application.CanteenOrders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.OrderDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.OrderQuantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.FoodItemName).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
