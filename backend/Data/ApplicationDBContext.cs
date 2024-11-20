//Student ID: 00015955
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDBContext : DbContext
{
  public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
  {
    //For linking code to database
  }
  
  public DbSet<Event> Events { get; set; }
  public DbSet<Comment> Comments { get; set; }
}