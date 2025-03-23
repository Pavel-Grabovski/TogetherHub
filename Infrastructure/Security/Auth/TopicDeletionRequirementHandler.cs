namespace Infrastructure.Security.Auth;

public class TopicDeletionRequirementHandler(
    IApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<TopicDeletionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TopicDeletionRequirement requirement)
    {
        string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            context.Fail();
        }

        RouteValueDictionary? routeValue = httpContextAccessor.HttpContext?.Request.RouteValues;
        var value = routeValue
            ?.FirstOrDefault(x => x.Key == "id")
            .Value?.ToString();

        if (string.IsNullOrEmpty(value))
        {
            context.Fail();
            return;
        }

        TopicId topicId = TopicId.Of(Guid.Parse(value));

        Topic? topic = await dbContext.Topics
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId);

        if (topic is not null && topic.AuthorId == userId)
        {
            context.Succeed(requirement);
        }
    }
}