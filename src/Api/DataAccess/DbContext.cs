using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Api.DataAccess;


public sealed class DbContext(IConfiguration configuration)  //primary constructor
{
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task Init()
    {
        using var connection = CreateConnection();
        await InitDrivers(connection);
    }
    
    private async Task InitDrivers(IDbConnection connection)
    {
        const string sql = """
                           
                                   CREATE TABLE IF NOT EXISTS
                                   Drivers (
                                       Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                       FirstName TEXT,
                                       LastName TEXT,
                                       Email TEXT,
                                       PhoneNumber TEXT
                                   );
                               
                           """;
        await connection.ExecuteAsync(sql);
    }
}