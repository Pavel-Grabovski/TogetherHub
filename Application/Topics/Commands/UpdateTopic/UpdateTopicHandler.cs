namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(
    IApplicationDbContext dbContext,
    IUserAccessor userAccessor)
    : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(
        UpdateTopicCommand request,
        CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(request.Id);

        Topic? topicDb = await dbContext.Topics
           .AsNoTracking()
           .FirstOrDefaultAsync(t => t.Id == topicId, cancellationToken);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(request.Id);

        string userId= userAccessor.GetUserId();

        if (topicDb.AuthorId != userId)
            throw new UserNotOrganizerException(topicId.Value, userId);

        topicDb = UpdateTopic(topicDb, request);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTopicResult(topicDb.ToTopicResponseDto());
    }

    private static Topic UpdateTopic(Topic topicDb, UpdateTopicCommand request)
    {
        Location location;
        if (request.UpdateTopicRequestDto.Location != null)
        {
            location = Location.Of(
                request.UpdateTopicRequestDto.Location.City,
                request.UpdateTopicRequestDto.Location.Street);
        }
        else
        {
            location = topicDb.Location;
        }

        topicDb = topicDb.Update(
            request.UpdateTopicRequestDto.Title,
            request.UpdateTopicRequestDto.Summary,
            request.UpdateTopicRequestDto.TopicType,
            location,
            request.UpdateTopicRequestDto.EventStart);
        return topicDb;
    }
}
