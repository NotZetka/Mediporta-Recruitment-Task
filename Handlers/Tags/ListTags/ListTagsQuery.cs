using MediatR;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsQuery : IRequest<IEnumerable<Tag>>
    {
        public int Size { get; set; }
        public OrderSelector OrderBy { get; set; }
        public bool Descending { get; set; }

    }
}
