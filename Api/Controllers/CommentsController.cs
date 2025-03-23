using Microsoft.AspNetCore.Mvc;
using Application.Comments.Commands;

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
}
