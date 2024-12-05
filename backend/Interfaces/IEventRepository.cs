//Student ID: 00015955
using backend.Dtos.Event;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces;

public interface IEventRepository
{
  Task<List<Event>> GetAllAsync(QueryObject query);
  Task<Event?> GetByIdAsync(int id);
  Task<Event> CreateAsync(Event eventModel, IFormFile? imageFile);
  Task<Event?> UpdateAsync(int id, UpdateEventRequestDto eventDto, IFormFile? imageFile);
  Task<Event?> DeleteAsync(int id);
  Task<bool> EventExists(int id);
}