namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicRequestDto CreateTopicRequestDto)
    : ICommand<CreateTopicResult>;
