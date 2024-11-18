using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
  private readonly ICommentRepository _commentRepo;
  public CommentController( ICommentRepository commentRepo)
  {
    _commentRepo = commentRepo;
  }
  [HttpGet]
  public async Task<IActionResult> GetAllComments()
  {
    var comments = await _commentRepo.GetAllAsync();
    var commentDto = comments.Select(e => e.ToCommentDto());
    return Ok(commentDto);
  }
}