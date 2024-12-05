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
    var eventDto = events.Select(e => e.ToEventDto(Request));
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
    return Ok(events.ToEventDto(Request));
  }

  [HttpPost]
  public async Task<IActionResult> CreateEvent([FromForm] CreateEventRequestDto eventDto, IFormFile imageFile)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    var eventModel = eventDto.ToEventFromCreateDTO();
    await _eventRepo.CreateAsync(eventModel, imageFile);
    return CreatedAtAction(nameof(GetEventById), new { id = eventModel.Id }, eventModel.ToEventDto(Request));
  }

  [HttpPut]
  [Route("{id:int}")]
  public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromForm] UpdateEventRequestDto updateDto, IFormFile imageFile)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    if (imageFile.Length == 0)
    {
      ModelState.AddModelError("ImageFile", "The uploaded file is empty.");
      return BadRequest(ModelState);
    }
    if (!imageFile.ContentType.StartsWith("image/"))
    {
      ModelState.AddModelError("ImageFile", "The uploaded file is not an image.");
      return BadRequest(ModelState);
    }
    
      
    var eventModel = await _eventRepo.UpdateAsync(id, updateDto, imageFile);
    if (eventModel == null)
    {
      return NotFound();
    }
    return Ok(eventModel.ToEventDto(Request));
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