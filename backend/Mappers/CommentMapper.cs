//Student ID: 00015955
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
  
  public static Comment ToCommentFromCreate(this CreateCommentRequestDto commentDto, int eventId)
  {
    return new Comment
    {
      Title = commentDto.Title,
      Content = commentDto.Content,
      EventId = eventId
    };
  }
  
  public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
  {
    return new Comment
    {
      Title = commentDto.Title,
      Content = commentDto.Content
    };
  }
}