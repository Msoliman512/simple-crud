using Api.DataAccess;
using Api.Middlewares;
using Dapper;

namespace Api.Features.Drivers.Services;

public class DriverValidationService(DbContext context, ILogger<DriverValidationService> logger) 
{
    
    public async Task EnsureEmailNotExistAsync(string email, int? id = null, CancellationToken cancellationToken = default!)
    {
        var normalizedEmail = email.ToLowerInvariant();
        var query = "SELECT COUNT(*) FROM Drivers WHERE Email = @Email";
        
        object parameters = new
        {
            Email = normalizedEmail
        };
        
        if(id.HasValue)
        {
            query += " AND Id != @Id";
            parameters = new
            {
                Email = normalizedEmail,
                Id = id.Value
            };
        }
        query += ";";
        
        try
        {
            using (var connection = context.CreateConnection())
            { 
                var count =  await connection.QueryFirstOrDefaultAsync<int>(query,  parameters);
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

    public async Task EnsurePhoneNumberNotExistAsync(string phoneNumber, int? id = null, CancellationToken cancellationToken = default!)
    {
        if(string.IsNullOrWhiteSpace(phoneNumber))
            return;
        
        var query = "SELECT COUNT(*) FROM Drivers WHERE PhoneNumber = @PhoneNumber";
        
        object parameters = new
        {
            PhoneNumber = phoneNumber
        };
        
        if(id.HasValue)
        {
            query += " AND Id != @Id";
            parameters = new
            {
                PhoneNumber = phoneNumber,
                Id = id.Value
            };
        }
        query += ";";
        
        try
        {
            using (var connection = context.CreateConnection())
            { 
                var count =  await connection.QueryFirstOrDefaultAsync<int>(query,  parameters);
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