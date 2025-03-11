namespace Application.Dtos;

public record CreateTopicRequestDto(
    string Title,
    string Summary,
    string TopicType,
    LocationRequestDto Location,
    DateTime EventStart
);
