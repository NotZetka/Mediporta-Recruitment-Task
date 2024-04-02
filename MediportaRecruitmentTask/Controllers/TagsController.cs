using MediatR;
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

        [HttpPost("List")]
        public async Task<IActionResult> ListTags([FromBody] ListTagsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("CountPercentage")]
        public async Task<IActionResult> CountPercentageShare([FromBody] CountPercentageShareQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
        [HttpGet("Reload")]
        public async Task<IActionResult> ReloadTags([FromQuery] int size)
        {
            var query = new ReloadTagsQuery() { Size = size };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}