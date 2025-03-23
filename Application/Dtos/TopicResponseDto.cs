namespace Application.Dtos;

public record TopicResponseDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    bool IsVoided,
    LocationResponseDto Location,
    DateTime? EventStart,
    List<UserProfileResponseDto> Users
);