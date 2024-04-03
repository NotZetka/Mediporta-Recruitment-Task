using Mediporta_Recruitment_Task.Handlers.Tags;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Tests.IntegrationTests
{
    public class TagsControllerTests : IDisposable
    {
        private MediportaRecruitmentTaskFactory _factory;
        private HttpClient _client;

        public TagsControllerTests()
        {
            _factory = new MediportaRecruitmentTaskFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ListTags_Should_ReturnProperData()
        {
            var queryData = new
            {
                Size = 100,
                OrderBy = ListTagsOrderSelector.Count,
                Descending = true,
                Page = 1
            };

            await _client.GetAsync("/Tags/Reload?size=1000");
            var response = await _client.PostAsync("/Tags/List",JsonContent.Create(queryData));
            var data = JsonConvert.DeserializeObject<IEnumerable<Tag>>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(data.Count(),queryData.Size);
        }
        

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
