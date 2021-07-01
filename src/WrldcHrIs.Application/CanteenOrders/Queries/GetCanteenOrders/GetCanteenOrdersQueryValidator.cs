using FluentValidation;

namespace WrldcHrIs.Application.CanteenOrders.Queries.GetCanteenOrders
{
    public class GetCanteenOrdersQueryValidator : AbstractValidator<GetCanteenOrdersQuery>
    {
        public GetCanteenOrdersQueryValidator()
        {
            // https://onthefencedevelopment.com/2013/02/07/validating-startend-dates-fluent-validation-mvc-4/
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must after Start date");
        }
    }
}
