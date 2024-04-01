using FluentValidation;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsQueryValidator : AbstractValidator<ListTagsQuery>
    {
        public ListTagsQueryValidator()
        {
            RuleFor(x=>x.Size).GreaterThanOrEqualTo(1);
            RuleFor(x=>x.Page).GreaterThanOrEqualTo(1);
        }
    }
}
