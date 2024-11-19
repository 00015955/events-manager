//Student ID: 00015955
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDBContext : IdentityDbContext<User>
{
  public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
  {
    //For linking code to database
  }
  
  public DbSet<Event> Events { get; set; }
  public DbSet<Comment> Comments { get; set; }
}