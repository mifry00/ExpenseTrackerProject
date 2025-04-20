using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Model.Entities;
using ExpenseTracker.Model.Repositories;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // POST: api/user/register
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        // Check if email already exists
        var existingUser = _userRepository.GetUserByEmail(user.Email);
        if (existingUser != null)
        {
            return Conflict("User already exists.");
        }

        // Hash password (in a real app, use BCrypt or similar!)
        user.PasswordHash = user.PasswordHash; // no hash for now (simplified)

        // Insert user
        _userRepository.InsertUser(user);

        return Ok("User registered successfully.");
    }

    // POST: api/user/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        var user = _userRepository.GetUserByEmail(loginUser.Email);
        if (user == null)
        {
            return Unauthorized("User not found.");
        }

        if (user.PasswordHash != loginUser.PasswordHash)
        {
            return Unauthorized("Invalid password.");
        }

        return Ok(new
        {
            message = "Login successful.",
            userId = user.Id,
            isAdmin = user.IsAdmin
        });
    }
}