namespace Application.Topics;

public interface ITopicsService
{
    public Task<List<Topic>> GetTopicsAsync();
    public Task<Topic> GetTopicByIdAsync(Guid id);
    public Task<Topic> CreateTopicAsync(Topic topicRequestDto);
    public Task<Topic> UpdateTopicAsync(TopicId id, Topic topicRequestDto);
    public Task<Topic> DeleteTopicAsync(TopicId id);

}
