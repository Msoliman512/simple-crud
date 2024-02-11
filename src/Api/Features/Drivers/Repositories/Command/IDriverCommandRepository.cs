using Api.Features.Drivers.Entities;

namespace Api.Features.Drivers.Repositories.Command;

public interface IDriverCommandRepository
{
    Task<int> CreateDriver(Driver driver);
    Task<bool> UpdateDriver(Driver driver);
    Task<bool> DeleteDriver(int id);
    Task<int> SeedRandomDrivers(int count);
}