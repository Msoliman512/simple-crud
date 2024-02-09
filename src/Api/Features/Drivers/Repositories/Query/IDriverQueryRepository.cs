using Api.Features.Drivers.Entities;

namespace Api.Features.Drivers.Repositories.Query;

public interface IDriverQueryRepository
{
    Task<Driver> GetDriverById(int id);
    Task<IEnumerable<Driver>> GetDrivers(int? pageIndex , int? pageSize, string? search, string? sortColumn, bool? orderByAsc);
}