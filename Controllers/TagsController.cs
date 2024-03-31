using MediatR;
using Mediporta_Recruitment_Task.Handlers.Tags.ListTags;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Compression;

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

        [HttpGet("List")]
        public async Task<IActionResult> ListTags([FromQuery]int size = 100)
        {
            var query = new ListTagsQuery { Size = size };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
