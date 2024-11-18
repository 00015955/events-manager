//Student ID: 00015955
namespace backend.Models;

public class Comment
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public DateTime CreatedOn { get; set; } = DateTime.Now;
  public int? EventId { get; set; } //Foreign Key
  public Event? Event { get; set; } //Navigation
}