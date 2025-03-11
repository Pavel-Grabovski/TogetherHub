using Application.Dtos;
using Application.Topics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController
    (ITopicsService topicsService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics()
    {
        return Ok(await topicsService.GetTopicsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id)
    {
        return Ok(await topicsService.GetTopicByIdAsync(id));
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicRequestDto dto)
    {
        return Ok(await topicsService.CreateTopicAsync(dto));
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid id, [FromBody]UpdateTopicRequestDto dto)
    {
        return Ok(await topicsService.UpdateTopicAsync(id, dto));
    }
}
