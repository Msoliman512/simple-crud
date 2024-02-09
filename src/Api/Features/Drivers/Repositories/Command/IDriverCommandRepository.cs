using Api.Features.Drivers.Entities;

namespace Api.Features.Drivers.Repositories.Command;

public interface IDriverCommandRepository
{
    Task<int> CreateDriver(Driver driver);
    Task UpdateDriver(Driver driver);
    Task DeleteDriver(int id);  
}