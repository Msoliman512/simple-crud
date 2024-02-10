using Api.DataAccess;
using Api.Middlewares;
using Dapper;

namespace Api.Features.Drivers.Services;

public class DriverValidationService(DbContext context, ILogger<DriverValidationService> logger) 
{
    
    public async Task EnsureEmailNotExistAsync(string email, CancellationToken cancellationToken = default!)
    {
        var normalizedEmail = email.ToLowerInvariant();
        var query = "SELECT COUNT(*) FROM Drivers WHERE Email = @Email;";
        
        try
        {
            using (var connection = context.CreateConnection())
            { 
                var count =  await connection.ExecuteAsync(query, new { Email = normalizedEmail });
                if (count > 0)
                {
                    throw new CustomException("Email already exists.");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while validating email");
            throw; 
        }
    }

    public async Task EnsurePhoneNumberNotExistAsync(string phoneNumber, CancellationToken cancellationToken = default!)
    {
        var query = "SELECT COUNT(*) FROM Drivers WHERE PhoneNumber = @Number;";
        
        try
        {
            using (var connection = context.CreateConnection())
            { 
                var count =  await connection.ExecuteAsync(query,  new { PhoneNumber = phoneNumber });
                if (count > 0)
                {
                    throw new CustomException("Phone number already exists.");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while validating phone number");
            throw; 
        }
    }
}