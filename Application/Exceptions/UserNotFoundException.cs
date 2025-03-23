namespace Application.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userId) 
        : base($"User with userId {userId} not found!")
    {
    }
}
