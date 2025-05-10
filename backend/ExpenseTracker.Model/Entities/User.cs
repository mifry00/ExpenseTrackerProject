namespace ExpenseTracker.Model.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; } // Primary key
    public string Email { get; set; } = ""; // Email of the user
    public string PasswordHash { get; set; } = ""; // Hashed password of the user
    public bool IsAdmin { get; set; } // Whether the user is an admin
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Date and time of creation
}
