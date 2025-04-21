using ExpenseTracker.Model.Repositories; // Added
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registering repository
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ExpenseRepository>();

var app = builder.Build();

// Enable swagger
    app.UseSwagger();
    app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "🚀 Expense Tracker API is running!");

app.Run();