using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using Microsoft.EntityFrameworkCore;

namespace Mediporta_Recruitment_Task.Database
{
    public class TagsContext : DbContext
    {
        public TagsContext(DbContextOptions<TagsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<TagEntity> Tags { get; set; }
    }
}
