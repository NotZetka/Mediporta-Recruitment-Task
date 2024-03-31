using MediatR;

namespace Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare
{
    public class CountPercentageShareQuery : IRequest<CountPercentageShareResponse>
    {
        public IEnumerable<string> Tags { get; set; }
    }
}
