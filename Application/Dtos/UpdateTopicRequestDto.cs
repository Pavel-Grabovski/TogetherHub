namespace Application.Dtos;

public record UpdateTopicRequestDto(
    string? Title,
    string? Summary,
    string? TopicType,
    LocationRequestDto? Location,
    DateTime? EventStart
);
