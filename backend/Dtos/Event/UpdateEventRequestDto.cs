//Student ID: 00015955
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Event;

public class UpdateEventRequestDto
{
  //Data Transfer without ID, StartDate and Comments
  [Required]
  [MinLength(3, ErrorMessage = "Event name must be at least 3 characters long.")]
  [MaxLength(50, ErrorMessage = "Event name must be less than 50 characters long.")]
  public string Name { get; set; } = string.Empty;
  [Required]
  [MinLength(3, ErrorMessage = "Event location must be at least 3 characters long.")]
  [MaxLength(100, ErrorMessage = "Event location must be less than 100 characters long.")]
  public string Location { get; set; } = string.Empty;
  public IFormFile? Image { get; set; }
  [Required]
  [MinLength(3, ErrorMessage = "Event description must be at least 3 characters long.")]
  [MaxLength(500, ErrorMessage = "Event description must be less than 500 characters long.")]
  public string Description { get; set; } = string.Empty;
}