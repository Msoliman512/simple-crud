using Api.DataAccess;
using Api.Features.Drivers.Entities;
using Bogus;
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

        public async Task<bool> UpdateDriver(Driver driver)
        {
            const string query = @"
                UPDATE Drivers
                SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber
                WHERE Id = @Id;";

            try
            {
                using (var connection = context.CreateConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(query, driver);
                    return rowsAffected > 0; 

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating driver");
                throw; 
            }
        }

        public async Task<bool> DeleteDriver(int id)
        {
            const string query = "DELETE FROM Drivers WHERE Id = @Id;";

            try
            {
                using (var connection = context.CreateConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting driver");
                throw; 
            }
        }
        
        public async Task<int> SeedRandomDrivers(int count)
        {
            const string query = @"
        INSERT INTO Drivers (FirstName, LastName, Email, PhoneNumber)
        VALUES (@FirstName, @LastName, @Email, @PhoneNumber);";

            try
            {
                using (var connection = context.CreateConnection())
                {
                    var driverFakerConfig = new Faker<Driver>()
                        .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                        .RuleFor(x => x.LastName, f => f.Name.LastName())
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("###########"));
                        
                    var drivers = driverFakerConfig.Generate(count);

                    // Execute the query with parameters for all drivers at once
                    return await connection.ExecuteAsync(query, drivers);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while seeding random drivers");
                throw;
            }
        }
    

}