using FluentValidation;
using System;

namespace WrldcHrIs.Application.CanteenOrders.Commands.EditOrder
{
    public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.OrderQuantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
