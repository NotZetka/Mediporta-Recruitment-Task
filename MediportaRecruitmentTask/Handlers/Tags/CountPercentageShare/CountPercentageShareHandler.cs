using MediatR;
using Mediporta_Recruitment_Task.Database;

namespace Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare
{
    public class CountPercentageShareHandler : IRequestHandler<CountPercentageShareQuery, CountPercentageShareResponse>
    {
        private readonly TagsContext _dbContext;
        public CountPercentageShareHandler(TagsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CountPercentageShareResponse> Handle(CountPercentageShareQuery request, CancellationToken cancellationToken)
        {
            var totalCount = _dbContext.Tags.Select(x => x.Count).Sum();
            var requestCount = _dbContext.Tags.Where(x => request.Tags.Contains(x.Name)).Select(x=>x.Count).Sum();
            var percentage = 100 * (float)requestCount / totalCount;
            var dict = new Dictionary<string, float>();
            foreach (var tag in request.Tags)
            {
                var count = _dbContext.Tags.FirstOrDefault(x => x.Name == tag)?.Count;
                if(count != null)
                {
                    dict.Add(tag, 100 * (float)count / totalCount);
                }
                else
                {
                    dict.Add(tag, 0);
                }
            }

            var response =  new CountPercentageShareResponse() { 
                Total = percentage,
                Individual = dict
            };
            return response;
        }
    }
}
