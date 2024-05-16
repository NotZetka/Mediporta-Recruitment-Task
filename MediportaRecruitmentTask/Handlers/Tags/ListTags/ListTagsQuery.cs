using MediatR;
using System.ComponentModel;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsQuery : IRequest<IEnumerable<Tag>>
    {
        public int Size { get; set; }

        [DefaultValue(ListTagsOrderSelector.Count)]
        public string? OrderBy { get; set; }

        [DefaultValue(true)]
        public bool Descending { get; set; }


        [DefaultValue(1)]
        public int Page { get; set; }
    }
}
