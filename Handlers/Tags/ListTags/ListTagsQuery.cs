using MediatR;
using Mediporta_Recruitment_Task.Models.StackOverflowTags;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsQuery : IRequest<IEnumerable<Tag>>
    {
        public int Size { get; set; }
    }
}
