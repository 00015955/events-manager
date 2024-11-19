using backend.Dtos.Event;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces;

public interface IEventRepository
{
  Task<List<Event>> GetAllAsync(QueryObject query);
  Task<Event?> GetByIdAsync(int id);
  Task<Event> CreateAsync(Event eventModel);
  Task<Event?> UpdateAsync(int id, UpdateEventRequestDto eventDto);
  Task<Event?> DeleteAsync(int id);
  Task<bool> EventExists(int id);
}