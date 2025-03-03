using Domain.Abstractions;
using Domain.ValueObjects;

namespace Domain.Model;

public class Topic : Entity<TopicId>
{
    public required string Title { get; set; }
    public DateTime? EventStart { get; set; }
    public required string Summary { get; set; }
    public required string TopicType { get; set; }
    public required Location Location { get; set; }


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
