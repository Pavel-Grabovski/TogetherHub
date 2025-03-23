using Domain.Exceptions;

namespace Application.Comments.Commands;

public class CreateCommentHandler(
    IApplicationDbContext dbContext,
    IUserAccessor userAccessor)
    : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    public async Task<CreateCommentResult> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(request.TopicId);
        Topic? topic = await dbContext.Topics
            .FindAsync(topicId, cancellationToken);

        if (topic is null || topic.IsDelete)
            throw new TopicNotFoundException(request.TopicId);

        string userId = userAccessor.GetUserId();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if(user is null) 
            throw new UserNotFoundException(userId);

        Comment comment = Comment.Create(
            commentId: CommentId.Of(Guid.NewGuid()),
            text: request.Text,
            topic: topic,
            author: user
        );

        topic.Comments.Add(comment);
        bool isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        if (!isSuccess)
            throw new CreateCommentException("Error creating comment");

        CommentResponseDto result = comment.ToCommentResponseDto();
        return new CreateCommentResult(result);
    }
}
