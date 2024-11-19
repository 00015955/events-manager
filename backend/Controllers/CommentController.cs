using backend.Dtos.Comment;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
  private readonly ICommentRepository _commentRepo;
  private readonly IEventRepository _eventRepo;
  public CommentController(ICommentRepository commentRepo, IEventRepository eventRepo)
  {
    _commentRepo = commentRepo;
    _eventRepo = eventRepo;
  }
  [HttpGet]
  public async Task<IActionResult> GetAllComments()
  {
    var comments = await _commentRepo.GetAllAsync();
    var commentDto = comments.Select(e => e.ToCommentDto());
    return Ok(commentDto);
  }
  
  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetCommentById([FromRoute] int id)
  {
    var comments = await _commentRepo.GetByIdAsync(id);
    if (comments == null)
    {
      return NotFound();
    }
    return Ok(comments.ToCommentDto());
  }

  [HttpPost]
  [Route("{eventId}")]
  public async Task<IActionResult> CreateComment([FromRoute] int eventId, CreateCommentRequestDto commentDto)
  {
    if (!await _eventRepo.EventExists(eventId))
    {
      return BadRequest("Event does not exist");
    }
    var commentModel = commentDto.ToCommentFromCreate(eventId);
    await _commentRepo.CreateAsync(commentModel);
    return CreatedAtAction(nameof(GetCommentById), new {id = commentModel.Id}, commentModel.ToCommentDto());
  }
}