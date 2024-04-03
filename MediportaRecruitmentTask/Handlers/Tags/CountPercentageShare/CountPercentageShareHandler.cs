using MediatR;
using Mediporta_Recruitment_Task.Database;
using Microsoft.IdentityModel.Tokens;

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
            var requestCount = !request.Tags.IsNullOrEmpty() ?
                    _dbContext.Tags.Where(x => request.Tags.Contains(x.Name)).Select(x=>x.Count).Sum()
                    :totalCount;
            var percentage = 100 * (float)requestCount / totalCount;
            var dict = new Dictionary<string, float>();
            if (!request.Tags.IsNullOrEmpty())
            {
                foreach (var tag in request.Tags)
                {
                    var count = _dbContext.Tags.FirstOrDefault(x => x.Name == tag)?.Count;
                    if (count != null)
                    {
                        dict.Add(tag, 100 * (float)count / totalCount);
                    }
                    else
                    {
                        dict.Add(tag, 0);
                    }
                }
            }
            else
            {
                foreach(var tag in _dbContext.Tags)
                {
                    dict.Add(tag.Name, 100 * (float)tag.Count / totalCount);
                }
            }
            var dictResponse = (request.OrderBy == CountPercentageOrderSelector.Name) ?
                ((request.Descending == false) ? dict.OrderBy(x => x.Key) : dict.OrderByDescending(x => x.Key)) :
                ((request.Descending == false) ? dict.OrderBy(x => x.Value) : dict.OrderByDescending(x => x.Value));
            var size = request.Size == 0 ? dictResponse.Count() : request.Size;
            var skip = request.Page > 1 ? size * (request.Page - 1) : 0;

            var response =  new CountPercentageShareResponse() { 
                Total = percentage,
                Individual = dictResponse.Skip(skip).Take(size).ToDictionary()
            };
            return response;
        }
    }
}
