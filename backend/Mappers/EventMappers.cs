//Student ID: 00015955
using backend.Dtos.Event;
using backend.Models;

namespace backend.Mappers;

public static class EventMappers
{
  public static EventDto ToEventDto(this Event eventModel)
  {
    return new EventDto
    {
      Id = eventModel.Id,
      Name = eventModel.Name,
      Location = eventModel.Location,
      Description = eventModel.Description,
      StartDate = eventModel.StartDate,
      Image = eventModel.Image,
      Comments = eventModel.Comments.Select(c => c.ToCommentDto()).ToList()
    };
  }

  public static Event ToEventFromCreateDTO(this CreateEventRequestDto eventRequestDto)
  {
    return new Event
    {
      Name = eventRequestDto.Name,
      Location = eventRequestDto.Location,
      Description = eventRequestDto.Description,
      StartDate = eventRequestDto.StartDate,
      Image = eventRequestDto.Image
    };
  }
}