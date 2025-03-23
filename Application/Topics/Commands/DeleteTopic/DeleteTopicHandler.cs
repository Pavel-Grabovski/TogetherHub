namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler(
    IApplicationDbContext dbContext,
    UserManager<User> userManager,
    IUserAccessor userAccessor)
    : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    public async Task<DeleteTopicResult> Handle(
        DeleteTopicCommand request,
        CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(request.Id);
        Topic? topicDb = await dbContext.Topics
            .FindAsync(topicId, cancellationToken);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(request.Id);

        string userId = userAccessor.GetUserId();
        User? user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new UserNotFoundException(userId);

        if (topicDb.AuthorId != userId)
            throw new UserNotOrganizerException(topicId.Value, userId);

        topicDb.IsDelete = true;
        topicDb.DeletionTime = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteTopicResult(true);
    }
}