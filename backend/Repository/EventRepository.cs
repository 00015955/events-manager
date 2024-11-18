using backend.Data;
using backend.Dtos.Event;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class EventRepository : IEventRepository
{
  private readonly ApplicationDBContext _context;
  public EventRepository(ApplicationDBContext context)
  {
    _context = context;
  }
  public Task<List<Event>> GetAllAsync()
  {
    return _context.Events.ToListAsync();
  }

  public async Task<Event?> GetByIdAsync(int id)
  {
    return await _context.Events.FindAsync(id);
  }

  public async Task<Event> CreateAsync(Event eventModel)
  {
    await _context.Events.AddAsync(eventModel);
    await _context.SaveChangesAsync();
    return eventModel;
  }
  
  public async Task<Event?> UpdateAsync(int id, UpdateEventRequestDto updateEventDto)
  {
    var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
    if (eventModel == null)
    {
      return null;
    }
    eventModel.Name = updateEventDto.Name;
    eventModel.Location = updateEventDto.Location;
    eventModel.StartDate = updateEventDto.StartDate;
    eventModel.Image = updateEventDto.Image;
    eventModel.Description = updateEventDto.Description;

    await _context.SaveChangesAsync();
    return eventModel;
  }

  public async Task<Event?> DeleteAsync(int id)
  {
    var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
    if (eventModel == null)
    {
      return null;
    }
    _context.Events.Remove(eventModel);
    await _context.SaveChangesAsync();
    return eventModel;
  }
}