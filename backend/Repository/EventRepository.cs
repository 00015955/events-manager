//Student ID: 00015955
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
  private readonly IWebHostEnvironment _environment;
  public EventRepository(ApplicationDBContext context, IWebHostEnvironment environment)
  {
    _context = context;
    _environment = environment;
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

  public async Task<Event> CreateAsync(Event eventModel, IFormFile? imageFile)
  {
    if (imageFile != null && imageFile.Length > 0)
    {
      string imagePath = await SaveImage(imageFile);
      eventModel.Image = imagePath;
    }
    
    await _context.Events.AddAsync(eventModel);
    await _context.SaveChangesAsync();
    return eventModel;
  }
  
  public async Task<Event?> UpdateAsync(int id, UpdateEventRequestDto updateEventDto, IFormFile? imageFile)
  {
    var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
    if (eventModel == null)
    {
      return null;
    }
    eventModel.Name = updateEventDto.Name;
    eventModel.Location = updateEventDto.Location;
    eventModel.Description = updateEventDto.Description;
    
    if (imageFile != null && imageFile.Length > 0)
    {

      if (!string.IsNullOrEmpty(eventModel.Image))
      {
        DeleteImage(eventModel.Image);
      }

      string imagePath = await SaveImage(imageFile);
      eventModel.Image = imagePath;
    }

    await _context.SaveChangesAsync();
    return eventModel;
  }

  public async Task<Event?> DeleteAsync(int id)
  {
    var eventModel = await _context.Events.Include(e => e.Comments).FirstOrDefaultAsync(x => x.Id == id);
    if (eventModel == null)
    {
      return null;
    }
    _context.Comments.RemoveRange(eventModel.Comments);
    _context.Events.Remove(eventModel);
    await _context.SaveChangesAsync();
    return eventModel;
  }

  public Task<bool> EventExists(int id)
  {
    return _context.Events.AnyAsync(e => e.Id == id);
  }
  
  // Helper methods for image handling
  private async Task<string> SaveImage(IFormFile imageFile)
  {
    if (string.IsNullOrEmpty(_environment.WebRootPath))
    {
      throw new Exception("WebRootPath is null or empty.");
    }
    
    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
    Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
      await imageFile.CopyToAsync(fileStream);
    }
    
    return $"/images/{uniqueFileName}";
  }

  private void DeleteImage(string imagePath)
  {
    var fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));
    if (File.Exists(fullPath))
    {
      File.Delete(fullPath);
    }
  }
}