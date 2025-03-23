namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(
    IApplicationDbContext dbContext,
    IUserAccessor userAccessor)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(
        CreateTopicCommand command,
        CancellationToken cancellationToken)
    {
        User user = await dbContext.Users
            .FirstAsync(us => us.UserName == userAccessor.GetUsername(), cancellationToken);

        Topic newTopic = CreateTopic(command.CreateTopicRequestDto, user.Id);

        Relationship relationship = Relationship.Create(
            id: RelationshipId.Of(Guid.NewGuid()),
            userId: user.Id,
            user: user,
            role: ParticipantRole.Organizer,
            topicId: newTopic.Id,
            topic: newTopic
        );

        newTopic.Users.Add(relationship);

        dbContext.Topics.Add(newTopic);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTopicResult(newTopic.ToTopicResponseDto());
    }

    private Topic CreateTopic(CreateTopicRequestDto dto, string authorId)
    {
        return Topic.Create(
            id: TopicId.Of(Guid.NewGuid()),
            title: dto.Title,
            eventStart: dto.EventStart,
            summary: dto.Summary,
            topicType: dto.TopicType,
            authorId: authorId,
            location: Location.Of(dto.Location.City, dto.Location.Street)
        );
    }
}