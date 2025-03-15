namespace Application.Dtos;

public record TopicResponseDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    LocationResponseDto Location,
    DateTime? EventStart,
    List<Relationship> Users
);