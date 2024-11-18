//Student ID: 00015955
namespace backend.Models;

public class Event
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Location { get; set; } = string.Empty;
  public DateTime StartDate { get; set; } = DateTime.Now;
  public string Image { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public List<Comment> Comments { get; set; } = new List<Comment>();
}