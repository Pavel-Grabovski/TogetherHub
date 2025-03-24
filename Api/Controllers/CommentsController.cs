using Microsoft.AspNetCore.Mvc;
using Application.Comments.Commands;
using Application.Comments.Queries;

namespace Api.Controllers;

public class CommentsController : TogetherControllerBase
{
    [HttpPost("create/{topicId}")]
    public async Task<IResult> CreateComment(
        Guid topicId,
        [FromBody] CommentRequestDto commentRequest,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(
            new CreateCommentCommand(topicId, commentRequest.Text), cancellationToken));
    }

    [HttpGet("{topicId}")]
    public async Task<IResult> GetComments(
        Guid topicId,
        CancellationToken cancellationToken)
    {
        return Results.Ok(await Mediator.Send(
            new GetCommentsQuery(topicId), cancellationToken));
    }
}
