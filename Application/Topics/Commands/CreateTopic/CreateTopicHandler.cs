using Domain.Enums;

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
            .FirstAsync(us => us.UserName == userAccessor.GetUsername());

        Topic newTopic = CreateTopic(command.CreateTopicRequestDto);

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

    private Topic CreateTopic(CreateTopicRequestDto dto)
    {
        return Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            dto.Title,
            dto.EventStart,
            dto.Summary,
            dto.TopicType,
            Location.Of(dto.Location.City, dto.Location.Street)
        );
    }
}