using ExpenseTracker.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ExpenseTracker.Model.Repositories;

public class ExpenseRepository : BaseRepository
{
    public ExpenseRepository(IConfiguration configuration) : base(configuration) { }

    public void AddExpense(Expense expense)
{
    using var conn = GetConnection();
    using var cmd = new NpgsqlCommand(@"
        INSERT INTO expenses (user_id, amount, category, description, expense_date, created_at)
        VALUES (@userId, @amount, @category, @description, @expenseDate, @createdAt)", conn);

    cmd.Parameters.AddWithValue("userId", expense.UserId);
    cmd.Parameters.AddWithValue("amount", expense.Amount);
    cmd.Parameters.AddWithValue("category", expense.Category);
    cmd.Parameters.AddWithValue("description", expense.Description ?? "");
    cmd.Parameters.AddWithValue("expenseDate", expense.ExpenseDate);
    cmd.Parameters.AddWithValue("createdAt", DateTime.UtcNow); // Add the current time

    conn.Open();
    cmd.ExecuteNonQuery();
}


    public List<Expense> GetExpensesByUserId(int userId)
{
    var expenses = new List<Expense>();
    using var conn = GetConnection();
    using var cmd = new NpgsqlCommand("SELECT id, user_id, amount, category, description, expense_date, is_approved, created_at FROM expenses WHERE user_id = @userId", conn);
    cmd.Parameters.AddWithValue("userId", userId);

    conn.Open();
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        expenses.Add(new Expense
        {
            Id = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            Amount = reader.GetDecimal(2),
            Category = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Description = reader.IsDBNull(4) ? null : reader.GetString(4),
            ExpenseDate = reader.GetDateTime(5),
            IsApproved = reader.GetBoolean(6),
            CreatedAt = reader.GetDateTime(7)
        });
    }

    return expenses;
}

// Get all unapproved expenses (for admin)
    public List<Expense> GetUnapprovedExpenses()
{
    var expenses = new List<Expense>();
    using var conn = GetConnection();
    using var cmd = new NpgsqlCommand("SELECT * FROM expenses WHERE is_approved = false", conn);
    
    conn.Open();
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        expenses.Add(new Expense
        {
            Id = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            Amount = reader.GetDecimal(2),
            Category = reader.GetString(3),
            Description = reader.GetString(4),
            ExpenseDate = reader.GetDateTime(5),
            IsApproved = reader.GetBoolean(6),
            CreatedAt = reader.GetDateTime(7)
        });
    }

    return expenses;
}

    // Delete a specific expense
    public void DeleteExpense(int id)
    {
    using var conn = GetConnection();
    using var cmd = new NpgsqlCommand("DELETE FROM expenses WHERE id = @id", conn);
    cmd.Parameters.AddWithValue("id", id);

    conn.Open();
    cmd.ExecuteNonQuery();
    }

    // Approve a specific expense
    public void ApproveExpense(int id)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand("UPDATE expenses SET is_approved = true WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", id);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
    
}
