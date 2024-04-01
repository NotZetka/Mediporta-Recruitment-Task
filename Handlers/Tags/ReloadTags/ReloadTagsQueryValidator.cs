using FluentValidation;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ReloadTags
{
    public class ReloadTagsQueryValidator : AbstractValidator<ReloadTagsQuery>
    {
        public ReloadTagsQueryValidator()
        {
            RuleFor(x=>x.Size).GreaterThanOrEqualTo(1);
        }
    }
}
