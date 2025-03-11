using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController
    (IMediator mediator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTopics()
    {
        return Results.Ok(await mediator.Send(new GetTopicsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetTopic(Guid id)
    {
        return Results.Ok(await mediator.Send(new GetTopicQuery(id)));
    }

    [HttpPost]
    [Route("create")]
    public async Task<IResult> CreateTopic(CreateTopicRequestDto dto)
    {
        CreateTopicResult result = await mediator.Send(new CreateTopicCommand(dto));
        string uri = $"/topics/{result.Result.Id}";

        return Results.Created(uri, result);
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
