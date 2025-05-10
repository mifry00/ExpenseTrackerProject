namespace ExpenseTracker.Model.Entities;

public class Expense
{
    public int Id { get; set; } // Primary key
    public int UserId { get; set; } // Foreign key to User table
    public decimal Amount { get; set; } // Amount of the expense
    public string Category { get; set; } = ""; // Category of the expense
    public string? Description { get; set; } // Description of the expense
    public DateTime ExpenseDate { get; set; } // Date of the expense
    public bool IsApproved { get; set; } // Approval status of the expense
    public DateTime CreatedAt { get; set; } // Date and time of creation
}
