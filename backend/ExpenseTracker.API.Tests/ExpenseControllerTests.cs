using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ExpenseTracker.API.Controllers;
using ExpenseTracker.Model.Entities;
using ExpenseTracker.Model.Repositories;
using System.Collections.Generic;

namespace ExpenseTracker.API.Tests;

[TestClass]
public class ExpenseControllerTests
{
    private Mock<IExpenseRepository> _mockExpenseRepository = null!;
    private ExpenseController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockExpenseRepository = new Mock<IExpenseRepository>();
        _controller = new ExpenseController(_mockExpenseRepository.Object);
    }

    // Test 1 to add an expense to the list
    [TestMethod]
    public void AddExpense_ShouldAddToList()
    {
        // Create a new expense
        var expense = new Expense
        { UserId = 1, Amount = 100, Category = "Food", Description = "Lunch" };

        // Add the expense to the list
        var result = _controller.AddExpense(expense);

        // Verify that the expense was added to the list
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        _mockExpenseRepository.Verify(repo => repo.AddExpense(expense), Times.Once);
    }

    // Test 2 to get expenses by user id
    [TestMethod]
    public void GetExpensesByUserId_ShouldReturnList()
    {
        // Create a list of expenses
        var expenses = new List<Expense>
        { new Expense { UserId = 1, Amount = 100, Category = "Food", Description = "Lunch" },
            new Expense { UserId = 1, Amount = 200, Category = "Transportation", Description = "Taxi" } };

        // Set up the mock expense repository to return the list of expenses
        _mockExpenseRepository.Setup(repo => repo.GetExpensesByUserId(1)).Returns(expenses);    

        // Get the expenses by user id
        var result = _controller.GetExpensesByUserId(1);

        // Verify that the list of expenses was returned
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        var returnedExpenses = (List<Expense>?)okResult.Value;
        Assert.IsNotNull(returnedExpenses);

        // Verify that the number of expenses is correct
        Assert.AreEqual(expenses.Count, returnedExpenses.Count);
    }

    // Test 3 to delete an expense from the list
    [TestMethod]
    public void DeleteExpense_ShouldRemoveFromList()
    {
        // Create an expense
        var expense = new Expense 
        { Id = 1, UserId = 1, Amount = 150, Category = "Utilities", Description = "Office supplies" };
    
        // Delete the expense
        var result = _controller.DeleteExpense(1);

        // Verify that the delete method was called
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        _mockExpenseRepository.Verify(repo => repo.DeleteExpense(1), Times.Once);
    }
}