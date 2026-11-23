using Microsoft.EntityFrameworkCore;
using SeptaTracker.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SeptaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SeptaDb")));

builder.Services.AddHttpClient<SeptaClient>(client =>
    client.BaseAddress = new Uri("https://www3.septa.org/api/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();