global using FrostedCornerWebAPI.Models;
global using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Services.FranchiseItemService;
using FrostedCornerWebAPI.Services.ItemService;
using FrostedCornerWebAPI.Services.OrderService;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UNCOMMENT LATER: To help connect w React app
// builder.Services.AddCors();

// For mapping Dtos
// Install NuGet Package in Package Manager: Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFranchiseItemService, FranchiseItemService>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
