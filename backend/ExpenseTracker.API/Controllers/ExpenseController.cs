using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Model.Entities;
using ExpenseTracker.Model.Repositories;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly ExpenseRepository _expenseRepository;

    public ExpenseController(ExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    // POST: api/expense/add
    [HttpPost("add")]
    public IActionResult AddExpense([FromBody] Expense expense)
    {
        if (expense == null)
        {
            return BadRequest("Invalid expense data.");
        }

        _expenseRepository.AddExpense(expense);  // Make sure AddExpense exists in repository
        return Ok(new { message = "Expense added successfully." });
    }

    // GET: api/expense/user/{userId}
    [HttpGet("user/{userId}")]
    public IActionResult GetExpensesByUserId(int userId)
    {
        try
        {
            var expenses = _expenseRepository.GetExpensesByUserId(userId);
            return Ok(expenses);
        }
        catch (Exception ex) /* Handle database or repository errors */
        {
            Console.WriteLine("Error fetching expenses: " + ex.Message);
            return StatusCode(500, "Server error: " + ex.Message);
        }
    }

    // DELETE: api/expense/{expenseId}
    [HttpDelete("{expenseId}")]
    public IActionResult DeleteExpense(int expenseId)
    {
        _expenseRepository.DeleteExpense(expenseId);
        return Ok(new { message = "Expense deleted successfully." });
    }

    // GET: api/expense/{expenseId}
    [HttpGet("{expenseId}")]
    public IActionResult GetExpenseById(int expenseId)
    {
        var expense = _expenseRepository.GetExpenseById(expenseId);
        if (expense == null)
            return NotFound();

        return Ok(expense);
    }


    // PUT: api/expense/{expenseId}
    [HttpPut("update")]
    public IActionResult UpdateExpense([FromBody] Expense updatedExpense)
    {
        if (updatedExpense == null || updatedExpense.Id == 0)
            return BadRequest("Invalid expense data.");

        _expenseRepository.UpdateExpense(updatedExpense);
        return Ok(new { message = "Expense updated successfully." });
    }

    // GET: api/expense/unapproved
    [HttpGet("unapproved")]
    public IActionResult GetUnapprovedExpenses()
    {
        var expenses = _expenseRepository.GetUnapprovedExpenses();
        return Ok(expenses);
    }

    // POST: approve expenses
    [HttpPost("approve/{id}")]
    public IActionResult ApproveExpense(int id)
    {
        _expenseRepository.ApproveExpense(id);
        return Ok(new { message = "Expense approved successfully." });
    }

    // GET: api/expense/approved
    [HttpGet("approved")]
    public IActionResult GetApprovedExpenses()
    {
        var expenses = _expenseRepository.GetApprovedExpenses();
        Console.WriteLine($"Retrieved {expenses.Count} approved expenses");
        foreach (var expense in expenses)
        {
            Console.WriteLine($"Expense {expense.Id}: isApproved = {expense.IsApproved}");
        }
        return Ok(expenses);
    }

    // POST: api/expense/unapprove/{id}
    [HttpPost("unapprove/{id}")]
    public IActionResult UnapproveExpense(int id)
    {
        _expenseRepository.UnapproveExpense(id);
        return Ok(new { message = "Expense unapproved successfully." });
    }
}
