using Mediporta_Recruitment_Task.Clients.TagsClient;
using Mediporta_Recruitment_Task.Database;
using Microsoft.EntityFrameworkCore;

namespace Mediporta_Recruitment_Task.Extentions
{
    public static class DatabaseExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using TagsContext dbContext =
                scope.ServiceProvider.GetRequiredService<TagsContext>();

            dbContext.Database.Migrate();
        }
        
        public static async void InitDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using TagsContext dbContext =
                scope.ServiceProvider.GetRequiredService<TagsContext>();

            var tags = dbContext.Tags;
            if(tags.Count()==0) {
                var tagsClient = scope.ServiceProvider.GetRequiredService<ITagsClient>();
                var tagsFromClient = await tagsClient.GetTags(1000);
                dbContext.Tags.AddRangeAsync(tagsFromClient.Select(x => new TagEntity
                {
                    Name = x.Name,
                    Count = x.Count 
                }));
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
