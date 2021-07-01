using FluentValidation;

namespace WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrdersForUserForUser
{
    public class GetCanteenOrdersForUserQueryValidator : AbstractValidator<GetCanteenOrdersForUserQuery>
    {
        public GetCanteenOrdersForUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must after Start date");
        }
    }
}
