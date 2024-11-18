//Student ID: 00015955
using backend.Data;
using backend.Dtos.Event;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
  private readonly ApplicationDBContext _context;
  public EventController(ApplicationDBContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllEvents()
  {
    var events = await _context.Events.ToListAsync();
    var eventDto = events.Select(e => e.ToEventDto());
    return Ok(events);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetEventById(int id)
  {
    var events = await _context.Events.FindAsync(id);
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
    await _context.Events.AddAsync(eventModel);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetEventById), new { id = eventModel.Id }, eventModel.ToEventDto());
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] UpdateEventRequestDto updateDto)
  {
    var eventModel = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    if (eventModel == null)
    {
      return NotFound();
    }
    eventModel.Name = updateDto.Name;
    eventModel.Location = updateDto.Location;
    eventModel.StartDate = updateDto.StartDate;
    eventModel.Image = updateDto.Image;
    eventModel.Description = updateDto.Description;

    await _context.SaveChangesAsync();
    return Ok(eventModel.ToEventDto());
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<IActionResult> DeleteEvent([FromRoute] int id)
  {
    var eventModel = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    if (eventModel == null)
    {
      return NotFound();
    }
    _context.Events.Remove(eventModel);
    await _context.SaveChangesAsync();
    return NoContent();
  }
}