using ExpenseTracker.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ExpenseTracker.Model.Repositories;

public class UserRepository : BaseRepository // Inherits from BaseRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    // Retrieves a user by email
    public User? GetUserByEmail(string email)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE email = @email", conn);
        cmd.Parameters.AddWithValue("email", email);

        conn.Open();
        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                IsAdmin = reader.GetBoolean(3),
                CreatedAt = reader.GetDateTime(4)
            };
        }

        return null;
    }

    // Inserts a new user into the database
    public void InsertUser(User user)
    {
        using var conn = GetConnection();
        using var cmd = new NpgsqlCommand(
            "INSERT INTO users (email, password_hash, is_admin) VALUES (@email, @PasswordHash, @isAdmin)", conn);
        
        cmd.Parameters.AddWithValue("email", user.Email);
        cmd.Parameters.AddWithValue("passwordHash", user.PasswordHash);
        cmd.Parameters.AddWithValue("isAdmin", user.IsAdmin);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}