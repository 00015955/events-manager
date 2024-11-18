//Student ID: 00015955
namespace backend.Dtos.Event;

public class UpdateEventRequestDto
{
  //Data Transfer without Id and Comments
  public string Name { get; set; } = string.Empty;
  public string Location { get; set; } = string.Empty;
  public DateTime StartDate { get; set; } = DateTime.Now;
  public string Image { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}