using MediatR;
using Mediporta_Recruitment_Task.Handlers.Tags;
using Mediporta_Recruitment_Task.Handlers.Tags.CountPercentageShare;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using Mediporta_Recruitment_Task.Handlers.Tags.ReloadTags;
using Microsoft.AspNetCore.Mvc;

namespace Mediporta_Recruitment_Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns list of tags
        /// </summary>
        /// <remarks>
        /// If size is not specyfied or 0 returns all tags. 
        /// Can be sorted by count or name. 
        /// Page specyfies how many tags should be skipped, for example size:100, page:2 meand skip tags 1-100 and return tags 101-200.
        /// </remarks>
        [HttpPost("List")]
        public async Task<ActionResult<IEnumerable<Tag>>> ListTags([FromBody] ListTagsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Returns percentage of tags from list 
        /// </summary>
        /// <remarks>
        /// If list is null or empty returns percentage for all tags
        /// If size is not specyfied or 0 returns all tags. 
        /// Page specyfies how many tags should be skipped, for example size:100, page:2 meand skip tags 1-100 and return tags 101-200.
        /// Can be sorted by percentage or name. 
        /// </remarks>
        [HttpPost("CountPercentage")]
        public async Task<ActionResult<CountPercentageShareResponse>> CountPercentageShare([FromBody] CountPercentageShareQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Reloads list of tags 
        /// </summary>
        /// <remarks>
        /// Reloads list of tags in particular size to database.
        /// </remarks>
        [HttpGet("Reload")]
        public async Task<IActionResult> ReloadTags([FromQuery] int size)
        {
            var query = new ReloadTagsQuery() { Size = size };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
