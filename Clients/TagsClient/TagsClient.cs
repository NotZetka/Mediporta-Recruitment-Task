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
                    try
                    {
                        var apiUrl = $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize={pageSize}&order=desc&sort=popular&site=stackoverflow";
                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();

                        if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                        {
                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                            using (StreamReader reader = new StreamReader(gzipStream))
                            {
                                string responseBody = await reader.ReadToEndAsync();
                                var deserializedResponse = JsonConvert.DeserializeObject<ListTagsResponse>(responseBody);
                                if (deserializedResponse != null)
                                {
                                    tags.AddRange(deserializedResponse.Items);
                                }
                            }
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Request exception: {e.Message}");
                    }
                    finally
                    {
                        page++;
                        remaining -= pageSize;
                    }
                }
                return tags;
            }
        }
    }
}
