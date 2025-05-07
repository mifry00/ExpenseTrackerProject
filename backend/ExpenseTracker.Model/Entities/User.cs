namespace ExpenseTracker.Model.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";

    [JsonPropertyName("passwordHash")]   
    public string PasswordHash { get; set; } = "";

    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
