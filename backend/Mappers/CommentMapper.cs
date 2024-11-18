using backend.Dtos.Comment;
using backend.Models;

namespace backend.Mappers;

public static class CommentMapper
{
  public static CommentDto ToCommentDto(this Comment commentModel)
  {
    return new CommentDto
    {
      Id = commentModel.Id,
      Title = commentModel.Title,
      Content = commentModel.Content,
      CreatedOn = commentModel.CreatedOn,
      EventId = commentModel.EventId
    };
  }
}