using ExpenseTracker.Model.Repositories; // Added
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registering repository
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ExpenseRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular frontend origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    app.UseCors(MyAllowSpecificOrigins);

}

// Enable swagger
    app.UseSwagger();
    app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "🚀 Expense Tracker API is running!");

app.Run();