namespace Application.Exceptions;

public class TopicNotFoundException : NotFoundException
{
    public TopicNotFoundException(Guid id) 
        : base($"Topic id:{id} not found.")
    {
    }
}
