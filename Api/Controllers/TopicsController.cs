using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/topics")]
[ApiController]
public class TopicsController(IMediator mediator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTopics(CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(new GetTopicsQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(new GetTopicQuery(id), cancellationToken));
    }

    [HttpPost("create")]
    public async Task<IResult> CreateTopic(CreateTopicRequestDto dto)
    {
        CreateTopicResult result = await mediator.Send(new CreateTopicCommand(dto));
        string uri = $"/topics/{result.Result.Id}";

        return Results.Created(uri, result);
    }

    [HttpPut("update/{id}")]
    public async Task<IResult> UpdateTopic(
        Guid id,
        [FromBody]UpdateTopicRequestDto dto,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(new UpdateTopicCommand(id, dto), cancellationToken));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteTopic(Guid id, CancellationToken cancellationToken)
    {
        return Results.Ok(await mediator.Send(new DeleteTopicCommand(id), cancellationToken));
    }
}
