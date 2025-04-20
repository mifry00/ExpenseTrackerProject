namespace ExpenseTracker.Model.Entities;

public class User
{
    public int Id { get; set; }                // user ID (primary key)
    public string Email { get; set; } = "";    // user email
    public string PasswordHash { get; set; } = ""; // hashed password
    public bool IsAdmin { get; set; }          // whether the user is an admin
    public DateTime CreatedAt { get; set; }    // when the user was added
}