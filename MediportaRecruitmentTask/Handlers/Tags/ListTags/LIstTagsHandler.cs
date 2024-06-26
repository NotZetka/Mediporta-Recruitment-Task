﻿using MediatR;
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

            if(request.OrderBy==ListTagsOrderSelector.Name)
            {
                tags = (request.Descending==false) ? tags.OrderBy(x => x.Name) : tags.OrderByDescending(x => x.Name);
            }
            else
            {
                tags = (request.Descending == false) ? tags.OrderBy(x => x.Count) : tags.OrderByDescending(x => x.Count);
            }

            var size = request.Size == 0 ? tagEntities.Count() : request.Size;
            var skip = request.Page > 1 ? size * (request.Page-1) : 0;
            tags = tags.Skip(skip).Take(size);

            return tags;
        }
    }
}
