using Mediporta_Recruitment_Task.Models.StackOverflowTags;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class ListTagsResponse
    {
        public bool HasMore { get; set; }
        public List<Tag> Items { get; set; }
        public int QuotaMax { get; set; }
        public int QuotaRemaining { get; set; }
    }
}
