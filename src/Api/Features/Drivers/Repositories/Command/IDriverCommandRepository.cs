using Api.Features.Drivers.Entities;

namespace Api.Features.Drivers.Repositories.Command;

public interface IDriverCommandRepository
{
    Task<int> CreateDriver(Driver driver);
    Task<int> UpdateDriver(Driver driver);
    Task<bool> DeleteDriver(int id);  
}