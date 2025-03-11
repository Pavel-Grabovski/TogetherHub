using Application.Dtos;

namespace Application.Topics;

public interface ITopicsService
{
    public Task<List<TopicResponseDto>> GetTopicsAsync();
    public Task<TopicResponseDto> GetTopicByIdAsync(Guid id);
    public Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto dto);
    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto dto);
    public Task<TopicResponseDto> DeleteTopicAsync(Guid id);

}
