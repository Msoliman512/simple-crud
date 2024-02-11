using Api.Common.Helpers;
using Api.Features.Drivers.Entities;
using Api.Features.Drivers.Repositories.Command;
using Api.Features.Drivers.Services;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;
/// <summary>
/// Represents the request for updating a driver.
/// </summary>
public sealed class UpdateDriverRequest : IRequest<UpdateDriverResponse>
{
    /// <summary>
    /// The ID of the driver to update.
    /// </summary>
    public required int Id { get; init; }
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
/// Represents the response for updating a driver.
/// </summary>
/// <param name="Result">update operation result</param>
public sealed record UpdateDriverResponse(bool Result);

public sealed class UpdateDriverValidator : AbstractValidator<UpdateDriverRequest>
{
    public UpdateDriverValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
        
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

public sealed class UpdateDriverHandler(IDriverCommandRepository repository, DriverValidationService driverValidationService) : IRequestHandler<UpdateDriverRequest, UpdateDriverResponse>
{

    public async Task<UpdateDriverResponse> Handle(UpdateDriverRequest request, CancellationToken cancellationToken)
    {

        await driverValidationService.EnsureEmailNotExistAsync(request.Email, request.Id, cancellationToken);
        await driverValidationService.EnsurePhoneNumberNotExistAsync(request.PhoneNumber ?? string.Empty, request.Id, cancellationToken);
        
        var driver = Driver.CreateInstance(
            id: request.Id,
            firstName: request.FirstName,
            lastName: request.LastName,
            email: request.Email,
            phoneNumber: request.PhoneNumber
        );


        var result = await repository.UpdateDriver(driver);
        return new UpdateDriverResponse(result);
    }
}