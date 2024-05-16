using Mediporta_Recruitment_Task.Handlers.Tags;
using Newtonsoft.Json;
using System.IO.Compression;

namespace Mediporta_Recruitment_Task.Clients.TagsClient
{
    public class TagsClient : ITagsClient
    {
        public async Task<IEnumerable<Tag>> GetTags(int size)
        {
            var remaining = size;
            var page = 1;

            using (HttpClient client = new HttpClient())
            {
                var tags = new List<Tag>();
                while (remaining > 0)
                {
                    var pageSize = remaining <= 100 ? remaining : 100;
                    var apiUrl = $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize={pageSize}&order=desc&sort=popular&site=stackoverflow";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    var deserializedResponse = JsonConvert.DeserializeObject<ListTagsResponse>(await response.Content.ReadAsStringAsync());
                    if (deserializedResponse != null)
                    {
                        tags.AddRange(deserializedResponse.Items);
                    }

                    page++;
                    remaining -= pageSize;
                }
                return tags;
            }
        }
    }
}
