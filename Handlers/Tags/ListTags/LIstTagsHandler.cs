using MediatR;
using Mediporta_Recruitment_Task.Models.StackOverflowTags;
using Newtonsoft.Json;
using System.Drawing;
using System.IO.Compression;

namespace Mediporta_Recruitment_Task.Handlers.Tags.ListTags
{
    public class LIstTagsHandler : IRequestHandler<ListTagsQuery, IEnumerable<Tag>>
    {
        public async Task<IEnumerable<Tag>> Handle(ListTagsQuery request, CancellationToken cancellationToken)
        {
            var page = 1;
            var remaining = request.Size;

            using (HttpClient client = new HttpClient())
            {
                var tags = new List<Tag>();
                while(remaining > 0)
                {
                    var size = remaining <= 100 ? remaining : 100;
                    try
                    {
                        var apiUrl = $"https://api.stackexchange.com/2.3/tags?page={page}&pagesize={size}&order=desc&sort=popular&site=stackoverflow";
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
                        remaining -= size;
                    }
                }
                return tags;
            }
        }
    }
}
