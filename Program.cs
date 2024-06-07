using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context DI
builder.Services.AddSqlite<TodoItemContext>("Data Source = DBFiles/TodoItem.db");

// DI repo
builder.Services.AddScoped<TodoItemRepository>();

// DI service
builder.Services.AddScoped<TodoItemService>();

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

app.MapGet("/", () => @"TodoApi management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();
