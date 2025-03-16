namespace Application.Security.Services;

public interface IUserAccessor
{
    public string GetUsername();

    public string GetUserId();
}