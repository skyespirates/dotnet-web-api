using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Interfaces;
using project_service.Repositories;
using project_service.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration == null)
{
    throw new Exception("Configuration is not initialized!");
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
