using FluentValidation;

namespace Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare
{
    public class CountPercentageShareQueryValidator : AbstractValidator<CountPercentageShareQuery>
    {
        public CountPercentageShareQueryValidator()
        {
            RuleFor(x => x.Size).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        }
    }
}
