using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using project_service.Data;
using project_service.Interfaces;
using project_service.Repositories;
using project_service.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration == null)
{
    throw new Exception("Configuration is not initialized!");
}

// Configure automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
    ! For now, I found it better using JsonSerializerOptions
    ! instead of using DTO
    ! this serializer will add properties like "$id" and "$values" in your JSON response
*/
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

/*
    ! With configuration below will remove "$id" and "$values" from JSON response
*/
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure to ignore cycles in object references
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.MaxDepth = 1000; // Set the maximum depth here
//     });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();


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
