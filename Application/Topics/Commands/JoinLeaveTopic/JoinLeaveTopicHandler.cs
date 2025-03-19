namespace Application.Topics.Commands.JoinLeaveTopic;

public class JoinLeaveTopicHandler(
    IApplicationDbContext dbContext,
    UserManager<User> userManager,
    IUserAccessor userAccessor)
    : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
{
    public async Task<JoinLeaveTopicResult> Handle(
        JoinLeaveTopicCommand request,
        CancellationToken cancellationToken)
    {
        Topic topic = await GetTopicAsync(request.Id, cancellationToken);
        User currentUser = await CetCurrentUserAsync(cancellationToken);

        User? organizer = topic.Users
            .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)?.CurrentUser;

        if (organizer is not null && organizer.Id == currentUser.Id)
            return await ToggleTopicStatusAsync(topic, cancellationToken);

        return await UpdateCurrentUserStatusAsync(topic, currentUser, cancellationToken);
    }

    private async Task<User> CetCurrentUserAsync(CancellationToken cancellationToken)
    {
        string userId = userAccessor.GetUserId();
        User? user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new UserNotFoundException(userId);
        return user;
    }

    private async Task<Topic> GetTopicAsync(Guid id, CancellationToken cancellationToken)
    {
        TopicId topicId = TopicId.Of(id);

        Topic? topicDb = await dbContext.Topics
          .AsNoTracking()
          .Include(t => t.Users)
          .ThenInclude(r => r.CurrentUser)
          .FirstOrDefaultAsync(t => t.Id == topicId, cancellationToken);

        if (topicDb is null || topicDb.IsDelete)
            throw new TopicNotFoundException(id);
        return topicDb;
    }

    private async Task<JoinLeaveTopicResult> ToggleTopicStatusAsync(
        Topic topic,
        CancellationToken cancellationToken)
    {
        bool oldStatus = topic.IsVoided;
        topic.IsVoided = !topic.IsVoided;

        dbContext.Topics.Update(topic);
        bool isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return new JoinLeaveTopicResult(
            $"Status has changed: {oldStatus} => {topic.IsVoided}",
            isSuccess);
    }

    private async Task<JoinLeaveTopicResult> UpdateCurrentUserStatusAsync(
        Topic topic,
        User currentUser,
        CancellationToken cancellationToken)
    {
        Relationship? joinUser = topic.Users
            .FirstOrDefault(u => u.CurrentUser.Id == currentUser.Id);

        string detail;

        if(joinUser is null)
        {
            Relationship relationship = Relationship.Create(
                id: RelationshipId.Of(Guid.NewGuid()),
                userId: currentUser.Id,
                user: currentUser,
                role: ParticipantRole.Participant,
                topicId: topic.Id,
                topic: topic
            );

            topic.Users.Add(relationship);
            detail = $"You have joined the topic({topic.Id.Value})";
        }
        else
        {
            topic.Users.Remove(joinUser);
            detail = $"You have left topic ({topic.Id.Value})";
        }
        dbContext.Topics.Update(topic);
        bool isSuccess = await dbContext
            .SaveChangesAsync (cancellationToken) > 0;

        return new JoinLeaveTopicResult(detail, isSuccess);
    }

}