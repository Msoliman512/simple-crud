using Api.DataAccess;
using Api.Features.Drivers.Entities;
using Dapper;

namespace Api.Features.Drivers.Repositories.Query;

public class DriverQueryRepository(DbContext context, ILogger<DriverQueryRepository> logger) : IDriverQueryRepository
{
   public async Task<Driver> GetDriverById(int id)
        {
            const string query = "SELECT * FROM Drivers WHERE Id = @Id";
            
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var driver = await connection.QuerySingleOrDefaultAsync<Driver>(query, new { Id = id });
                    return driver;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while fetching driver by ID: {Id}", id);
                throw; 
            }
        }

        public async Task<IEnumerable<Driver>> GetDrivers(int? pageIndex, int? pageSize, string? search, string? sortColumn, bool? orderByAsc)
        {
            try
            {
                // Construct the base query
                string query = "SELECT * FROM Drivers";
                int offset = 0;

                // Add WHERE clause for search filtering if search term is provided
                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Email LIKE @Search OR PhoneNumber LIKE @Search";
                }

                // Add ORDER BY clause for sorting; null no ordering is required, true ASC, false DESC
                if (orderByAsc.HasValue)
                    query += $" ORDER BY {sortColumn} {(orderByAsc.Value ? "ASC" : "DESC")}";

                // Add LIMIT and OFFSET for pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    // Calculate offset based on pageIndex and pageSize for pagination
                    offset = (pageIndex.Value - 1) * pageSize.Value;
                    query += " LIMIT @PageSize OFFSET @Offset";
                }

                // Parameters for SQL query
                var parameters = new { Search = $"%{search}%", PageSize = pageSize, Offset = offset };

                using (var connection = context.CreateConnection())
                {
                    var drivers = await connection.QueryAsync<Driver>(query, parameters);
                    return drivers;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while fetching drivers");
                throw; 
            }
        }
}