using LibrarySystem.Application;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Persistence;
using LibrarySystem.Persistence.Context;
using LibrarySystem.Persistence.Implementations.Repositories;
using LibrarySystem.Persistence.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using MovieAPI.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices()
    .AddPersistenceServices(builder.Configuration);

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

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
