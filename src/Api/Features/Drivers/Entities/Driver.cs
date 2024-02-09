namespace Api.Features.Drivers.Entities;

public sealed class Driver
{
    private Driver() { }

    public int Id { get; init; }

    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; set; }

    public string? PhoneNumber { get; private set; }
    
    public static Driver CreateInstance
    (
        int id,
        string firstName,
        string lastName ,
        string email ,
        string? phoneNumber = null
    )
    {
        var instance = new Driver
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        return instance;
    }
    
}