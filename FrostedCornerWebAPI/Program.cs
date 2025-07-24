global using FrostedCornerWebAPI.Data;
global using FrostedCornerWebAPI.Models;
using FrostedCornerWebAPI.Services.FranchiseItemService;
using FrostedCornerWebAPI.Services.ItemService;
using FrostedCornerWebAPI.Services.OrderService;
using FrostedCornerWebAPI.Services.SuppliesService;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"CONNECTION STRING: {builder.Configuration.GetConnectionString("DefaultConnection")}");

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// For mapping Dtos
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFranchiseItemService, FranchiseItemService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISuppliesService, SuppliesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

// Apply the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
