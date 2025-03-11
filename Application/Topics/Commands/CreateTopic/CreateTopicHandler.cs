namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(
    IApplicationDbContext dbContext,
    IMapper mapper)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(
        CreateTopicCommand command,
        CancellationToken cancellationToken)
    {
        Topic newTopic = CreateTopic(command.CreateTopicRequestDto);

        dbContext.Topics.Add(newTopic);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTopicResult(mapper.Map<TopicResponseDto>(newTopic));
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