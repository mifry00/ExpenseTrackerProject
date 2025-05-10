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
    // HTTP request received from frontend to register a new user
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        Console.WriteLine($"REGISTERING USER: email={user.Email}, passwordHash={user.PasswordHash}");

        // Check if email already exists
        var existingUser = _userRepository.GetUserByEmail(user.Email);
        if (existingUser != null)
        {
            return Conflict("User already exists.");
        }

        _userRepository.InsertUser(user);

        return Ok(new { message = "User registered successfully." });
    }

    // POST: api/user/login
    // HTTP request received from frontend to login a user
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        Console.WriteLine($"LOGIN attempt for {loginUser.Email} with passwordHash={loginUser.PasswordHash}");

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
