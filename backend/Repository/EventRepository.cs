using backend.Data;
using backend.Dtos.Event;
using backend.Helpers;
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
  public async Task<List<Event>> GetAllAsync(QueryObject query)
  {
    var events =  _context.Events.Include(c => c.Comments).AsQueryable();
    //Filtering by Event Name
    if (!string.IsNullOrWhiteSpace(query.Name))
    {
      events = events.Where(e => e.Name.Contains(query.Name));
    }
    
    //Sorting by Event Name
    if (!string.IsNullOrWhiteSpace(query.SortBy))
    {
      if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
      {
        events = query.IsDescending ? events.OrderByDescending(e => e.Name) : events.OrderBy(e => e.Name);
      }
    }
    
    //Pagination
    var skipNumber = (query.PageNumber - 1) * query.PageSize;
    
    return await events.Skip(skipNumber).Take(query.PageSize).ToListAsync();
  }

  public async Task<Event?> GetByIdAsync(int id)
  {
    return await _context.Events.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
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

  public Task<bool> EventExists(int id)
  {
    return _context.Events.AnyAsync(e => e.Id == id);
  }
}