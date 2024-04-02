using MediatR;
using Mediporta_Recruitment_Task.Database;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsHandler : IRequestHandler<ListTagsQuery, IEnumerable<Tag>>
    {
        private readonly TagsContext _dbContext;

        public ListTagsHandler(TagsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Tag>> Handle(ListTagsQuery request, CancellationToken cancellationToken)
        {
            var tagEntities = _dbContext.Tags.ToArray();
            var tags = tagEntities.Select(x => new Tag
            {
                Name = x.Name,
                Count = x.Count,
            });

            if(request.OrderBy==OrderSelector.Name)
            {
                tags = (request.Descending==false) ? tags.OrderBy(x => x.Name) : tags.OrderByDescending(x => x.Name);
            }
            else
            {
                tags = (request.Descending == false) ? tags.OrderBy(x => x.Count) : tags.OrderByDescending(x => x.Count);
            }

            var skip = request.Page > 1 ? request.Size * (request.Page-1) : 0;
            tags = tags.Skip(skip).Take(request.Size);

            return tags;
        }
    }
}
