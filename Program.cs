using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixMinApi.Data;
using SixMinApi.Dtos;
using SixMinApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// //Building sql connection
var sqlConBuilder = new SqlConnectionStringBuilder();


sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlConBuilder.UserID = builder.Configuration["UserID"];
sqlConBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());// registering the automapper.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/v1/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, int id) =>
{
    var command = await repo.GetCommandById(id);
    if (command != null)
    {
        return Results.Ok(mapper.Map<CommandReadDto>(command));
    }
    return Results.NotFound();
});

app.MapPost("api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto cmdCreateDto) =>
{
    var commandModel = mapper.Map<Command>(cmdCreateDto);
    await repo.CreateCommand(commandModel);
    await repo.SafeChanges();

});

app.Run();
