//Student ID: 00015955
namespace backend.Dtos.Comment;

public class CommentDto
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int? EventId { get; set; }
}