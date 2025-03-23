
namespace Application.Exceptions;

public class UserNotOrganizerException : ForbiddenException
{
    public UserNotOrganizerException(string message) : base(message)
    {
    }
    public UserNotOrganizerException()
    {
    }

    public UserNotOrganizerException(Guid topicId, string userId)
        : base($"User with Id:{userId} is not the organizer of the topic with id:{topicId}")
    {

    }
}