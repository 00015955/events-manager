//Student ID: 00015955
using backend.Data;
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
    var events = _context.Events.ToList();
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
    return Ok(events);
  }
}