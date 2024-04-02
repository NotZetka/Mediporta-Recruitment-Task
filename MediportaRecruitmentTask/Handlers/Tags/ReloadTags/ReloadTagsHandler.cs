using MediatR;
using Mediporta_Recruitment_Task.Clients.TagsClient;
using Mediporta_Recruitment_Task.Database;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ReloadTags
{
    public class ReloadTagsHandler : IRequestHandler<ReloadTagsQuery, Unit>
    {
        private readonly TagsContext _dbContext;
        private readonly ITagsClient _tagsClient;

        public ReloadTagsHandler(TagsContext dbContext, ITagsClient tagsClient)
        {
            _dbContext = dbContext;
            _tagsClient = tagsClient;
        }
        public async Task<Unit> Handle(ReloadTagsQuery request, CancellationToken cancellationToken)
        {
            var tagsToRemove = _dbContext.Tags;
            _dbContext.Tags.RemoveRange(tagsToRemove);
            var tags = await _tagsClient.GetTags(request.Size);
            await _dbContext.Tags.AddRangeAsync(tags.Select(x => new TagEntity
            {
                Name = x.Name,
                Count = x.Count
            }));
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
