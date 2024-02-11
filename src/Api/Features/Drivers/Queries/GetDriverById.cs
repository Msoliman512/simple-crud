
using Api.Common.Helpers;
using Api.Features.Drivers.Repositories.Query;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Queries;

/// <summary>
/// Represents the request for getting driver by Id.
/// </summary>
public sealed class GetDriverByIdRequest : IRequest<GetDriverByIdResponse>
{
    /// <summary>
    /// The ID of the driver to get.
    /// </summary>
    public required int Id { get; init; }
}

/// <summary>
/// Represents the response for getting a driver by Id.
/// </summary>
/// <param name="Id">driver Id</param>
/// <param name="FirstName">driver first name</param>
/// <param name="LastName">driver last name</param>
/// <param name="Email">driver email</param>
/// <param name="PhoneNumber">driver phone number</param>
/// <param name="AlphabetizedFullName">the alphabetized full name of the driver, where each part (first name and last name) is sorted alphabetically.</param>
public sealed record GetDriverByIdResponse(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, string AlphabetizedFullName);
public sealed class GetDriverByIdValidator : AbstractValidator<GetDriverByIdRequest>
{
    public GetDriverByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");
    }
}

public sealed class GetDriverByIdHandler(IDriverQueryRepository repository) : IRequestHandler<GetDriverByIdRequest, GetDriverByIdResponse>
{

    public async Task<GetDriverByIdResponse> Handle(GetDriverByIdRequest request, CancellationToken cancellationToken)
    {
        var driver = await repository.GetDriverById(request.Id);
        
        // Alphabetize the first name and last name
        var alphabetizedFirstName = new string(driver.FirstName.ToLowerInvariant().OrderBy(c => c).ToArray());
        var alphabetizedLastName = new string(driver.LastName.ToLowerInvariant().OrderBy(c => c).ToArray());
        var alphabetizedFullName = $"{alphabetizedFirstName} {alphabetizedLastName}";
        
        return new GetDriverByIdResponse(driver.Id, driver.FirstName, driver.LastName, driver.Email,
            driver.PhoneNumber, alphabetizedFullName);
    }
}