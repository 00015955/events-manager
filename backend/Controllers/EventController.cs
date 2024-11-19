//Student ID: 00015955
using backend.Dtos.Event;
using backend.Helpers;
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
  public async Task<IActionResult> GetAllEvents([FromQuery] QueryObject query)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState); //Data Validation
    var events = await _eventRepo.GetAllAsync(query);
    var eventDto = events.Select(e => e.ToEventDto());
    return Ok(eventDto);
  }

  [HttpGet]
  [Route("{id:int}")]
  public async Task<IActionResult> GetEventById([FromRoute] int id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
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
    if (!ModelState.IsValid) return BadRequest(ModelState);
    var eventModel = eventDto.ToEventFromCreateDTO();
    await _eventRepo.CreateAsync(eventModel);
    return CreatedAtAction(nameof(GetEventById), new { id = eventModel.Id }, eventModel.ToEventDto());
  }

  [HttpPut]
  [Route("{id:int}")]
  public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] UpdateEventRequestDto updateDto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    var eventModel = await _eventRepo.UpdateAsync(id, updateDto);
    if (eventModel == null)
    {
      return NotFound();
    }
    return Ok(eventModel.ToEventDto());
  }

  [HttpDelete]
  [Route("{id:int}")]
  public async Task<IActionResult> DeleteEvent([FromRoute] int id)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    var eventModel = await _eventRepo.DeleteAsync(id);
    if (eventModel == null)
    {
      return NotFound();
    }
    return NoContent();
  }
}