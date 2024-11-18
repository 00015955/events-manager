//Student ID: 00015955
using backend.Data;
using backend.Dtos.Event;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

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
  public IActionResult GetAllEvents()
  {
    var events = _context.Events.ToList().Select(e => e.ToEventDto());
    return Ok(events);
  }

  [HttpGet("{id}")]
  public IActionResult GetEventById(int id)
  {
    var events = _context.Events.Find(id);
    if (events == null)
    {
      return NotFound();
    }
    return Ok(events.ToEventDto());
  }

  [HttpPost]
  public IActionResult CreateEvent([FromBody] CreateEventRequestDto eventDto)
  {
    var eventModel = eventDto.ToEventFromCreateDTO();
    _context.Events.Add(eventModel);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetEventById), new { id = eventModel.Id }, eventModel.ToEventDto());
  }

  [HttpPut]
  [Route("{id}")]
  public IActionResult UpdateEvent([FromRoute] int id, [FromBody] UpdateEventRequestDto updateDto)
  {
    var eventModel = _context.Events.FirstOrDefault(e => e.Id == id);
    if (eventModel == null)
    {
      return NotFound();
    }
    eventModel.Name = updateDto.Name;
    eventModel.Location = updateDto.Location;
    eventModel.StartDate = updateDto.StartDate;
    eventModel.Image = updateDto.Image;
    eventModel.Description = updateDto.Description;

    _context.SaveChanges();
    return Ok(eventModel.ToEventDto());
  }
}