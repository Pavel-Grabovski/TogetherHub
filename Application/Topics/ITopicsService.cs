namespace Application.Topics;

public interface ITopicsService
{
    public Task<List<Topic>> GetTopicsAsync();
    public Task<Topic> GetTopicByIdAsync(Guid id);
    public Task<Topic> CreateTopicAsync(Topic topicRequestDto);
    public Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto);
    public Task<Topic> DeleteTopicAsync(Guid id);

}
