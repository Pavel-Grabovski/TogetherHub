using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController
    (IMediator mediator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics()
    {
        return Ok(await mediator.Send(new GetTopicsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id)
    {
        return Ok(null);
        //return Ok(await topicsService.GetTopicByIdAsync(id));
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicRequestDto dto)
    {
        return Ok(null);
        //return Ok(await topicsService.CreateTopicAsync(dto));
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid id, [FromBody]UpdateTopicRequestDto dto)
    {
        return Ok(null);
        //return Ok(await topicsService.UpdateTopicAsync(id, dto));
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult> DeleteTopic(Guid id)
    {
        //await topicsService.DeleteTopicAsync(id);
        return NoContent();
    }
}
