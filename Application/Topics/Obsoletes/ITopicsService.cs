namespace Application.Topics.Obsoletes;

[Obsolete("All Obsolete", true)]
public interface ITopicsService
{
    public Task<List<TopicResponseDto>> GetTopicsAsync();
    public Task<TopicResponseDto> GetTopicByIdAsync(Guid id);
    public Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto dto);
    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto dto);
    public Task DeleteTopicAsync(Guid id);

}
