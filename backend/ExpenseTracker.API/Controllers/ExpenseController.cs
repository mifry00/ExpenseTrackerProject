using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Model.Entities;
using ExpenseTracker.Model.Repositories;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseRepository _expenseRepository;

    // Constructor injection of ExpenseRepository
    public ExpenseController(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    // POST: api/expense/add
    // HTTP request received from frontend to add a new expense
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
    // HTTP request received from frontend to get all expenses for a user
    [HttpGet("user/{userId}")]
    public IActionResult GetExpensesByUserId(int userId)
    {
        try
        {
            var expenses = _expenseRepository.GetExpensesByUserId(userId);
            return Ok(expenses);
        }
        catch (Exception ex) // Handle database or repository errors 
        {
            Console.WriteLine("Error fetching expenses: " + ex.Message);
            return StatusCode(500, "Server error: " + ex.Message);
        }
    }

    // DELETE: api/expense/{expenseId}
    // HTTP request received from frontend to delete an expense
    [HttpDelete("{expenseId}")]
    public IActionResult DeleteExpense(int expenseId)
    {
        _expenseRepository.DeleteExpense(expenseId);
        return Ok(new { message = "Expense deleted successfully." });
    }

    // GET: api/expense/{expenseId}
    // HTTP request received from frontend to get an expense by ID
    [HttpGet("{expenseId}")]
    public IActionResult GetExpenseById(int expenseId)
    {
        var expense = _expenseRepository.GetExpenseById(expenseId);
        if (expense == null)
            return NotFound();

        return Ok(expense);
    }


    // PUT: api/expense/update
    // HTTP request received from frontend to update an expense
    [HttpPut("update")]
    public IActionResult UpdateExpense([FromBody] Expense updatedExpense)
    {
        if (updatedExpense == null || updatedExpense.Id == 0)
            return BadRequest("Invalid expense data.");

        _expenseRepository.UpdateExpense(updatedExpense);
        return Ok(new { message = "Expense updated successfully." });
    }

    // GET: api/expense/unapproved
    // HTTP request received from frontend to get all unapproved expenses for admin
    [HttpGet("unapproved")]
    public IActionResult GetUnapprovedExpenses()
    {
        try
        {
            var expenses = _expenseRepository.GetUnapprovedExpenses();
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching unapproved expenses: {ex.Message}");
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

    // POST: api/expense/approve/{id}
    // HTTP request received from frontend to approve an expense for admin
    [HttpPost("approve/{id}")]
    public IActionResult ApproveExpense(int id)
    {
        try
        {
            _expenseRepository.ApproveExpense(id);
            return Ok(new { message = "Expense approved successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error approving expense: {ex.Message}");
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

    // GET: api/expense/approved
    // HTTP request received from frontend to get all approved expenses for admin
    [HttpGet("approved")]
    public IActionResult GetApprovedExpenses()
    {
        try
        {
            var expenses = _expenseRepository.GetApprovedExpenses();
            Console.WriteLine($"Retrieved {expenses.Count} approved expenses");
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Expense {expense.Id}: isApproved = {expense.IsApproved}");
            }
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching approved expenses: {ex.Message}");
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

    // POST: api/expense/unapprove/{id}
    // HTTP request received from frontend to unapprove/delete an expense for admin
    [HttpPost("unapprove/{id}")]
    public IActionResult UnapproveExpense(int id)
    {
        _expenseRepository.UnapproveExpense(id);
        return Ok(new { message = "Expense unapproved successfully." });
    }
}
