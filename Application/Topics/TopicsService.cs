using Application.Data.DataBaseContext;
using Application.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicsService(
    IApplicationDbContext dbContext,
    ILogger<TopicsService> logger,
    IMapper mapper) 
    : ITopicsService
{
    public async Task<List<TopicResponseDto>> GetTopicsAsync()
    {
        List<Topic> topicsDb = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync();

        List<TopicResponseDto> topicsResponse = mapper.Map<List<TopicResponseDto>>(topicsDb);

        return topicsResponse;
    }

    public Task<TopicResponseDto> GetTopicByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<TopicResponseDto> DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }


    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }
}