using MediatR;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
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
        public async Task<IActionResult> ListTags([FromQuery] ListTagsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
