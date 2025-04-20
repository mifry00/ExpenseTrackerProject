namespace ExpenseTracker.Model.Entities;

public class Expense
{
    public int Id { get; set; }
    public int UserId { get; set; }                     // foreign key to User
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime ExpenseDate { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set;}

    }