using backend.Models;

namespace backend.Interfaces;

public interface ICommentRepository
{
  Task<List<Comment>> GetAllAsync();
  Task<Comment?> GetByIdAsync(int id);
}