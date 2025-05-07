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
            INSERT INTO expenses (user_id, amount, category, description, expense_date, created_at, is_approved)
            VALUES (@userId, @amount, @category, @description, @expenseDate, @createdAt, @isApproved)", conn);

        cmd.Parameters.AddWithValue("userId", expense.UserId);
        cmd.Parameters.AddWithValue("amount", expense.Amount);
        cmd.Parameters.AddWithValue("category", expense.Category);
        cmd.Parameters.AddWithValue("expenseDate", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("createdAt", DateTime.UtcNow); 
        cmd.Parameters.AddWithValue("description", expense.Description ?? "");
        cmd.Parameters.AddWithValue("isApproved", false); // Set default value to false

        conn.Open();
        cmd.ExecuteNonQuery();
    }

    // Get all  expenses by user
    public List<Expense> GetExpensesByUserId(int userId)
    {
        var expenses = new List<Expense>();
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(@"
    SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
    FROM expenses 
    WHERE user_id = @userId", conn);   
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
                Category = reader.GetString(3),
                ExpenseDate = reader.GetDateTime(4),
                IsApproved = reader.GetBoolean(5),
                CreatedAt = reader.GetDateTime(6),
                Description = reader.GetString(7)
            });
        }

        return expenses;
    }

    // Get all unapproved expenses (for admin)
    public List<Expense> GetUnapprovedExpenses()
    {
        var expenses = new List<Expense>();
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(@"
        SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
        FROM expenses 
        WHERE is_approved = false", conn);

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
                ExpenseDate = reader.GetDateTime(4),
                IsApproved = reader.GetBoolean(5),
                CreatedAt = reader.GetDateTime(6),
                Description = reader.GetString(7)
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

    public Expense GetExpenseById(int expenseId)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(@"
        SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
        FROM expenses 
        WHERE id = @id", conn);

        cmd.Parameters.AddWithValue("id", expenseId);

        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Expense
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Amount = reader.GetDecimal(2),
                Category = reader.GetString(3),
                ExpenseDate = reader.GetDateTime(4),
                IsApproved = reader.GetBoolean(5),
                CreatedAt = reader.GetDateTime(6),
                Description = reader.GetString(7)
            };
        }

        return null!;
    }

    

    // Edit/update a specific expense
    public void UpdateExpense(Expense expense)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(@"
        UPDATE expenses 
        SET amount = @amount, category = @category, description = @description, expense_date = @expenseDate 
        WHERE id = @id", conn);

        cmd.Parameters.AddWithValue("amount", expense.Amount);
        cmd.Parameters.AddWithValue("category", expense.Category);
        cmd.Parameters.AddWithValue("description", expense.Description ?? "");
        cmd.Parameters.AddWithValue("expenseDate", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("id", expense.Id);

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

    // Get all approved expenses (for admin)
    public List<Expense> GetApprovedExpenses()
    {
        var expenses = new List<Expense>();
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(@"
            SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
            FROM expenses 
            WHERE is_approved = true", conn);

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
                ExpenseDate = reader.GetDateTime(4),
                IsApproved = reader.GetBoolean(5),
                CreatedAt = reader.GetDateTime(6),
                Description = reader.GetString(7)
            });
        }

        return expenses;
    }

    // Unapprove a specific expense
    public void UnapproveExpense(int id)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand("UPDATE expenses SET is_approved = false WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", id);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
