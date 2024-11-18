using backend.Dtos.Event;
using backend.Models;

namespace backend.Mappers;

public static class EventMappers
{
  public static EventDto ToEventDto(this Event eventModel)
  {
    return new EventDto()
    {
      Id = eventModel.Id,
      Name = eventModel.Name,
      Location = eventModel.Location,
      Description = eventModel.Description,
      StartDate = eventModel.StartDate,
      Image = eventModel.Image
    };
  }
}