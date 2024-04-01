using FluentValidation;

namespace Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare
{
    public class CountPercentageShareQueryValidator : AbstractValidator<CountPercentageShareQuery>
    {
        public CountPercentageShareQueryValidator()
        {
            RuleFor(x => x.Tags).NotEmpty();
            RuleFor(x => x.Tags).NotNull();
        }
    }
}
