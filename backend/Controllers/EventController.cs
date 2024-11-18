//Student ID: 00015955
using backend.Dtos.Event;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
  private readonly IEventRepository _eventRepo;
  public EventController( IEventRepository eventRepo)
  {
    _eventRepo = eventRepo;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllEvents()
  {
    var events = await _eventRepo.GetAllAsync();
    var eventDto = events.Select(e => e.ToEventDto());
    return Ok(eventDto);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetEventById([FromRoute] int id)
  {
    var events = await _eventRepo.GetByIdAsync(id);
    if (events == null)
    {
      return NotFound();
    }
    return Ok(events.ToEventDto());
  }

  [HttpPost]
  public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequestDto eventDto)
  {
    var eventModel = eventDto.ToEventFromCreateDTO();
    await _eventRepo.CreateAsync(eventModel);
    return CreatedAtAction(nameof(GetEventById), new { id = eventModel.Id }, eventModel.ToEventDto());
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] UpdateEventRequestDto updateDto)
  {
    var eventModel = await _eventRepo.UpdateAsync(id, updateDto);
    if (eventModel == null)
    {
      return NotFound();
    }
    return Ok(eventModel.ToEventDto());
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<IActionResult> DeleteEvent([FromRoute] int id)
  {
    var eventModel = await _eventRepo.DeleteAsync(id);
    if (eventModel == null)
    {
      return NotFound();
    }
    return NoContent();
  }
}