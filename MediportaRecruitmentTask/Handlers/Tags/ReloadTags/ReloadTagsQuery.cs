using MediatR;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ReloadTags
{
    public class ReloadTagsQuery : IRequest<Unit>
    {
        public int Size { get; set; }
    }
}
