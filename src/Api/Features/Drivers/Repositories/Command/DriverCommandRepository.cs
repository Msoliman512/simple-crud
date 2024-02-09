using Api.DataAccess;
using Api.Features.Drivers.Entities;
using Dapper;

namespace Api.Features.Drivers.Repositories.Command;

public class DriverCommandRepository(DbContext context, ILogger<DriverCommandRepository> logger) : IDriverCommandRepository
{
      public async Task<int> CreateDriver(Driver driver)
        {
            const string query = @"
                INSERT INTO Drivers (FirstName, LastName, Email, PhoneNumber)
                VALUES (@FirstName, @LastName, @Email, @PhoneNumber);
                SELECT last_insert_rowid();"; // SQLite-specific function to get the last inserted row ID

            try
            {
                using (var connection = context.CreateConnection())
                {
                    var driverId = await connection.ExecuteScalarAsync<int>(query, driver);
                    return driverId;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while creating driver");
                throw; 
            }
        }

        public async Task UpdateDriver(Driver driver)
        {
            const string query = @"
                UPDATE Drivers
                SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber
                WHERE Id = @Id;";

            try
            {
                using (var connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, driver);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating driver");
                throw; 
            }
        }

        public async Task DeleteDriver(int id)
        {
            const string query = "DELETE FROM Drivers WHERE Id = @Id;";

            try
            {
                using (var connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting driver");
                throw; 
            }
        }
    

}