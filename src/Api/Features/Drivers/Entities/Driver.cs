namespace Api.Features.Drivers.Entities;

public sealed class Driver
{
    public Driver() { }

    public int Id { get; init; }

    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public string? PhoneNumber { get;  set; }
    
    public static Driver CreateInstance
    (
        int? id = null,
        string? firstName = null,
        string? lastName = null ,
        string? email = null ,
        string? phoneNumber = null
    )
    {
        var instance = new Driver
        {
            Id = id.GetValueOrDefault(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        return instance;
    }
    
}