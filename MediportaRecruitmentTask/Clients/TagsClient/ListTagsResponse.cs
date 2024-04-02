using Mediporta_Recruitment_Task.Handlers.Tags;

namespace Mediporta_Recruitment_Task.Clients.TagsClient
{
    public class ListTagsResponse
    {
        public bool HasMore { get; set; }
        public List<Tag> Items { get; set; }
        public int QuotaMax { get; set; }
        public int QuotaRemaining { get; set; }
    }
}
