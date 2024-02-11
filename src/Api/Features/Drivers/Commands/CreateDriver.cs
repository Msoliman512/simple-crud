using Api.Common.Helpers;
using Api.Features.Drivers.Entities;
using Api.Features.Drivers.Repositories.Command;
using Api.Features.Drivers.Services;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;
/// <summary>
/// Represents the request for creating a driver.
/// </summary>
public sealed class CreateDriverRequest : IRequest<CreateDriverResponse>
{
    /// <summary>
    /// The first name of the driver.
    /// </summary>
    public required string FirstName { get; init; }
    /// <summary>
    /// The last name of the driver.
    /// </summary>
    public required string LastName { get; init; }
    /// <summary>
    /// The email of the driver.
    /// </summary>
    public required string Email { get; init; }
    /// <summary>
    /// The phone number of the driver.
    /// </summary>
    public string? PhoneNumber { get; init; }
}

/// <summary>
/// Represents the response for creating a driver.
/// </summary>
/// <param name="Id">id of the added driver</param>
public sealed record CreateDriverResponse(int Id);

public sealed class CreateDriverValidator : AbstractValidator<CreateDriverRequest>
{
    public CreateDriverValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First Name must not be empty.")
            .MaximumLength(Constants.NamesMaxLength)
            .WithMessage("First Name must not exceed 50 characters.");
            
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last Name must not be empty.")
            .MaximumLength(Constants.NamesMaxLength)
            .WithMessage("Last Name must not exceed 50 characters.");
        
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("Email must not be empty.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
            
        RuleFor(x => x.PhoneNumber)
            .Must(phoneNumber => phoneNumber.IsValidPhoneNumber())
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");
    }
}

public sealed class CreateDriverHandler(IDriverCommandRepository repository, DriverValidationService driverValidationService) : IRequestHandler<CreateDriverRequest, CreateDriverResponse>
{

    public async Task<CreateDriverResponse> Handle(CreateDriverRequest request, CancellationToken cancellationToken)
    {

        await driverValidationService.EnsureEmailNotExistAsync(request.Email, null, cancellationToken);
        await driverValidationService.EnsurePhoneNumberNotExistAsync(request.PhoneNumber ?? string.Empty, null, cancellationToken);
        
        var driver = Driver.CreateInstance(
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email,
            phoneNumber: request.PhoneNumber
            );


        var id = await repository.CreateDriver(driver);
        return new CreateDriverResponse(id);
    }
}