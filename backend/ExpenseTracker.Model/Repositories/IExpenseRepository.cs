using ExpenseTracker.Model.Entities;

namespace ExpenseTracker.Model.Repositories;

public interface IExpenseRepository
{
    void AddExpense(Expense expense);
    List<Expense> GetExpensesByUserId(int userId);
    void DeleteExpense(int expenseId);
    Expense? GetExpenseById(int expenseId);
    void UpdateExpense(Expense expense);
    List<Expense> GetUnapprovedExpenses();
    void ApproveExpense(int id);
    List<Expense> GetApprovedExpenses();
    void UnapproveExpense(int id);
} 