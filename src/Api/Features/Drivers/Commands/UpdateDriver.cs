using Api.Common.Helpers;
using Api.Features.Drivers.Entities;
using Api.Features.Drivers.Repositories.Command;
using Api.Features.Drivers.Services;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;

public sealed class UpdateDriverRequest : IRequest<UpdateDriverResponse>
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? PhoneNumber { get; init; }
}

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