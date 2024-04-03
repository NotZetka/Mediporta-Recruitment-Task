using MediatR;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using System.ComponentModel;

namespace Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare
{
    public class CountPercentageShareQuery : IRequest<CountPercentageShareResponse>
    {
        public IEnumerable<string>? Tags { get; set; }

        public int Size { get; set; }

        [DefaultValue(CountPercentageOrderSelector.Percentage)]
        public CountPercentageOrderSelector OrderBy { get; set; }

        [DefaultValue(true)]
        public bool Descending { get; set; }


        [DefaultValue(1)]
        public int Page { get; set; }
    }
}
