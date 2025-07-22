global using FrostedCornerWebAPI.Models;
global using FrostedCornerWebAPI.Services;
global using FrostedCornerWebAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UNCOMMENT LATER: To help connect w React app
// builder.Services.AddCors();

// For mapping Dtos
// Install NuGet Package in Package Manager: Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<FranchiseItemService>();
builder.Services.AddScoped<ItemService>();

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
