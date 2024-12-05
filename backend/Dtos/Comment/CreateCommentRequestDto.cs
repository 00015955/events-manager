//Student ID: 00015955
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Comment;

public class CreateCommentRequestDto
{
  [Required]
  [MinLength(3, ErrorMessage = "Comment title must be at least 3 characters long.")]
  [MaxLength(50, ErrorMessage = "Comment title must be less than 100 characters long.")]
  public string Title { get; set; } = string.Empty;
  [Required]
  [MinLength(3, ErrorMessage = "Comment content must be at least 3 characters long.")]
  [MaxLength(250, ErrorMessage = "Comment content must be less than 250 characters long.")]
  public string Content { get; set; } = string.Empty;
}