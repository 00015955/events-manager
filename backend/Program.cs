//Student ID: 00015955
using DotNetEnv;
using backend.Data;
using backend.Interfaces;
using backend.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
    policy =>
    {
      policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

Env.Load();

//Mac or Linux
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
  .Replace("{DB_HOST}", Env.GetString("DB_HOST"))
  .Replace("{DB_NAME}", Env.GetString("DB_NAME"))
  .Replace("{DB_USER}", Env.GetString("DB_USER"))
  .Replace("{DB_PASS}", Env.GetString("DB_PASS"));

//Windows
/*var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
  .Replace("{PCNAME}", Env.GetString("PCNAME"))
  .Replace("{DATABASENAME}", Env.GetString("DATABASENAME"));*/

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
  options.UseSqlServer(defaultConnectionString);
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
