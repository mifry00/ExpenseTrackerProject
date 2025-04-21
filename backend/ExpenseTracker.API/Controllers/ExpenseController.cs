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

    [HttpPost]
    public IActionResult AddExpense([FromBody] Expense expense)
    {
        _expenseRepository.InsertExpense(expense);
        return Ok("Expense added successfully.");
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserExpenses(int userId)
    {
        var expenses = _expenseRepository.GetExpensesByUserId(userId);
        return Ok(expenses);
    }

    // GET: /api/expense/unapproved
[HttpGet("unapproved")]
public IActionResult GetUnapprovedExpenses()
{
    var unapproved = _expenseRepository.GetUnapprovedExpenses();
    return Ok(unapproved);
}

// PUT: /api/expense/approve/{id}
[HttpPut("approve/{id}")]
public IActionResult ApproveExpense(int id)
{
    _expenseRepository.ApproveExpense(id);
    return Ok($"Expense {id} approved.");
}
}
