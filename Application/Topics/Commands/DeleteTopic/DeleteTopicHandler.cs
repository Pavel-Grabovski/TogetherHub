
namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler(
    IApplicationDbContext dbContext)
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

        topicDb.IsDelete = true;
        topicDb.DeletionTime = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteTopicResult(true);
    }
}