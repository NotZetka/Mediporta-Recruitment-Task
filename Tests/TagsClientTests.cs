using Mediporta_Recruitment_Task.Clients.TagsClient;

namespace Tests.UnitTests
{
    public class TagsClientTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(100)]
        public async void Client_Should_ReturnProperAmount(int size)
        {
            var client = new TagsClient();
            var result = await client.GetTags(size);

            Assert.NotNull(result);
            Assert.Equal(size, result.Count());
        }

    }
}
