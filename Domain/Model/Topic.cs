namespace Domain.Model;

public class Topic : Entity<TopicId>
{
    public required string Title { get; set; } = default!;
    public DateTime? EventStart { get; set; } = default!;
    public required string Summary { get; set; } = default!;
    public required string TopicType { get; set; } = default!;
    public required Location Location { get; set; } = default!;


    public static Topic Create(
        TopicId id,
        string title,
        DateTime eventStart,
        string summary,
        string topicType,
        Location location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(summary);
        ArgumentException.ThrowIfNullOrWhiteSpace(topicType);

        if (eventStart.Kind != DateTimeKind.Utc)
            eventStart = eventStart.ToUniversalTime();

        Topic topic = new Topic
        {
            Id = id,
            Title = title,
            EventStart = eventStart,
            Summary = summary,
            TopicType = topicType,
            Location = location
        };

        return topic;
    }
}
