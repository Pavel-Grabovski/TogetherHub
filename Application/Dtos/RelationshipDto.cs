using Domain.Enums;

namespace Application.Dtos;

public record RelationshipDto
(
    RelationshipId Id,
    TopicId TopicReference,
    string UserReference,
    ParticipantRole Role,

    TopicResponseDto TopicDto,
    UserProfileResponseDto UserDto
);