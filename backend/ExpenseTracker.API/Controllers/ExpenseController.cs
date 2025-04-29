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
    catch (Exception ex) //temporarily!!!
    {
        Console.WriteLine("🚨 Error fetching expenses: " + ex.Message);
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
}
