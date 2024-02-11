using Api.Common.Helpers;
using Api.Features.Drivers.Entities;
using Api.Features.Drivers.Repositories.Command;
using Api.Features.Drivers.Services;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;

public sealed class CreateDriverRequest : IRequest<CreateDriverResponse>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? PhoneNumber { get; init; }
}

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