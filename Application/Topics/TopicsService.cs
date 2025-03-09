using Application.Data.DataBaseContext;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicsService(
    IApplicationDbContext dbContext,
    ILogger<TopicsService> logger) 
    : ITopicsService
{
    public async Task<List<Topic>> GetTopicsAsync()
    {
        List<Topic> topics = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync();

        return topics;
    }

    public Task<Topic> GetTopicByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }
    public Task<Topic> UpdateTopicAsync(TopicId id, Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> DeleteTopicAsync(TopicId id)
    {
        throw new NotImplementedException();
    }


}