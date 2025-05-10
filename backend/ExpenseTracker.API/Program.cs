using ExpenseTracker.Model.Repositories; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ExpenseTracker.API.Middleware;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<UserRepository>();

// Configure CORS properly
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Use CORS BEFORE authorization
app.UseCors(MyAllowSpecificOrigins);

// Swagger setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseBasicAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Expense Tracker API is running!");

app.Run();
