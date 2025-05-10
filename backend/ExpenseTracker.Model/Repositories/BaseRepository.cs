using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ExpenseTracker.Model.Repositories;

public class BaseRepository
{
    protected readonly string ConnectionString;

    // Constructor injection of IConfiguration
    public BaseRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("AppProgDb")!;
    }

    protected NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(ConnectionString);
    }

    protected NpgsqlDataReader ExecuteReader(NpgsqlCommand cmd)
    {
        var conn = GetConnection();
        cmd.Connection = conn;
        conn.Open();
        return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
    }

    protected int ExecuteNonQuery(NpgsqlCommand cmd)
    {
        using var conn = GetConnection();
        cmd.Connection = conn;
        conn.Open();
        return cmd.ExecuteNonQuery();
    }
}