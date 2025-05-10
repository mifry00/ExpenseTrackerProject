using ExpenseTracker.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ExpenseTracker.Model.Repositories;

// Handles database operations for expenses
public class ExpenseRepository : BaseRepository, IExpenseRepository
{
    public ExpenseRepository(IConfiguration configuration) : base(configuration) { }

    // Creates new expense (defaults to unapproved)
    public void AddExpense(Expense expense)
    {
        using var cmd = new NpgsqlCommand(@"
            INSERT INTO expenses (user_id, amount, category, description, expense_date, created_at, is_approved)
            VALUES (@userId, @amount, @category, @description, @expenseDate, @createdAt, @isApproved)");

        cmd.Parameters.AddWithValue("userId", expense.UserId);
        cmd.Parameters.AddWithValue("amount", expense.Amount);
        cmd.Parameters.AddWithValue("category", expense.Category);
        cmd.Parameters.AddWithValue("expenseDate", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("createdAt", DateTime.UtcNow); 
        cmd.Parameters.AddWithValue("description", expense.Description ?? "");
        cmd.Parameters.AddWithValue("isApproved", false); // Set default value to false

        ExecuteNonQuery(cmd);
    }

    // Gets all expenses for a specific user
    public List<Expense> GetExpensesByUserId(int userId)
    {
        var expenses = new List<Expense>();
        using var cmd = new NpgsqlCommand(@"
            SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
            FROM expenses 
            WHERE user_id = @userId");   
        cmd.Parameters.AddWithValue("userId", userId);

        using var reader = ExecuteReader(cmd);
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

    // Gets all expenses pending admin approval
    public List<Expense> GetUnapprovedExpenses()
    {
        var expenses = new List<Expense>();
        using var cmd = new NpgsqlCommand(@"
            SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
            FROM expenses 
            WHERE is_approved = false");

        try
        {
            using var reader = ExecuteReader(cmd);
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
        catch (Exception ex)
        {
            Console.WriteLine($"Database error in GetUnapprovedExpenses: {ex.Message}");
            throw;
        }
    }

    // Deletes expense from database
    public void DeleteExpense(int id)
    {
        using var cmd = new NpgsqlCommand("DELETE FROM expenses WHERE id = @id");
        cmd.Parameters.AddWithValue("id", id);
        ExecuteNonQuery(cmd);
    }

    // Gets single expense by ID
    public Expense GetExpenseById(int expenseId)
    {
        using var cmd = new NpgsqlCommand(@"
            SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
            FROM expenses 
            WHERE id = @id");
        cmd.Parameters.AddWithValue("id", expenseId);

        using var reader = ExecuteReader(cmd);
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

    // Updates existing expense details
    public void UpdateExpense(Expense expense)
    {
        using var cmd = new NpgsqlCommand(@"
            UPDATE expenses 
            SET amount = @amount, category = @category, description = @description, expense_date = @expenseDate 
            WHERE id = @id");

        cmd.Parameters.AddWithValue("amount", expense.Amount);
        cmd.Parameters.AddWithValue("category", expense.Category);
        cmd.Parameters.AddWithValue("description", expense.Description ?? "");
        cmd.Parameters.AddWithValue("expenseDate", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("id", expense.Id);

        ExecuteNonQuery(cmd);
    }

    // Marks expense as approved by admin
    public void ApproveExpense(int id)
    {
        using var cmd = new NpgsqlCommand("UPDATE expenses SET is_approved = true WHERE id = @id");
        cmd.Parameters.AddWithValue("id", id);
        ExecuteNonQuery(cmd);
    }

    // Gets all approved expenses
    public List<Expense> GetApprovedExpenses()
    {
        var expenses = new List<Expense>();
        using var cmd = new NpgsqlCommand(@"
            SELECT id, user_id, amount, category, expense_date, is_approved, created_at, description 
            FROM expenses 
            WHERE is_approved = true");

        try
        {
            using var reader = ExecuteReader(cmd);
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
        catch (Exception ex)
        {
            Console.WriteLine($"Database error in GetApprovedExpenses: {ex.Message}");
            throw;
        }
    }

    // Reverts expense approval status
    public void UnapproveExpense(int id)
    {
        using var cmd = new NpgsqlCommand("UPDATE expenses SET is_approved = false WHERE id = @id");
        cmd.Parameters.AddWithValue("id", id);
        ExecuteNonQuery(cmd);
    }
}
