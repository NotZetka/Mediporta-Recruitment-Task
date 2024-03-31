using Mediporta_Recruitment_Task.Handlers.Tags;

namespace Mediporta_Recruitment_Task.Clients.TagsClient
{
    public interface ITagsClient
    {
        public Task<IEnumerable<Tag>> GetTags(int remaining);
    }
}