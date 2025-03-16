namespace Application.Extensions;

public static class TopicExtensions
{
    public static TopicResponseDto ToTopicResponseDto(
        this Topic topic)
    {
        return new TopicResponseDto(
            Id: topic.Id.Value,
            Title: topic.Title,
            Summary: topic.Summary,
            TopicType: topic.TopicType,
            Location: new LocationResponseDto(
                topic.Location.City,
                topic.Location.City),
            EventStart: topic.EventStart,
            IsVoided: topic.IsVoided,
            Users: topic.Users.Select(r => new UserProfileResponseDto(
                Id: r.CurrentUser.Id,
                UserName: r.CurrentUser.UserName!,
                Email: r.CurrentUser.Email!,
                FullName: r.CurrentUser.FullName,
                Role: r.Role.ToString()
            )).ToList());
    }

    public static List<TopicResponseDto> ToTopicResponseDtoList(
        this IEnumerable<Topic> topics)
    {
        return topics.Select(t => t.ToTopicResponseDto()).ToList();
    }
}
