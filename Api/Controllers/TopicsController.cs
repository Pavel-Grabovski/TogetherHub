using Api.Controllers;
using Application.Topics.Commands.JoinLeaveTopic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TopicsController : TogetherControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTopics(CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(new GetTopicsQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(new GetTopicQuery(id), cancellationToken));
    }

    [HttpPost("create")]
    public async Task<IResult> CreateTopic(CreateTopicRequestDto dto)
    {
        CreateTopicResult result = await Mediator.Send(new CreateTopicCommand(dto));
        string uri = $"/topics/{result.Result.Id}";

        return Results.Created(uri, result);
    }

    [HttpPut("update/{id}")]
    public async Task<IResult> UpdateTopic(
        Guid id,
        [FromBody]UpdateTopicRequestDto dto,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(new UpdateTopicCommand(id, dto), cancellationToken));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IResult> DeleteTopic(Guid id, CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(new DeleteTopicCommand(id), cancellationToken));
    }

    [HttpPost("join/{id}")]
    public async Task<IResult> JoinLeaveTopic(Guid id, CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(new JoinLeaveTopicCommand(id)));
    }
}
