using Api.Common.Helpers;
using Api.Features.Drivers.Repositories.Query;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Queries;

/// <summary>
/// Represents the request for getting drivers.
/// </summary>
public sealed class GetDriversRequest : IRequest<GetDriversResponse>
{
    /// <summary>Pagination: page index default value is 1</summary>
    public int PageIndex { get; init; } = 1;
    /// <summary>Pagination: page size default value is 10</summary>
    public int PageSize { get; init; } = 10;
    /// <summary>Search keyword</summary>
    public string? Keyword { get; init; }   
    /// <summary>Column name to sort by</summary>
    public string? OrderByColumn { get; init; }
    /// <summary>Sort direction; null no search required, true Ascending, and false Descending</summary>
    public bool? OrderBy { get; init; } 
}

/// <summary>
/// 
/// </summary>
/// <param name="TotalCount">total count of drivers</param>
/// <param name="Items"> fetched items</param>
public sealed record GetDriversResponse(int TotalCount, GetDriverItem[] Items);

/// <summary>
/// 
/// </summary>
/// <param name="Id">driver Id</param>
/// <param name="FirstName">driver first name</param>
/// <param name="LastName">driver last name</param>
/// <param name="Email">driver email</param>
/// <param name="PhoneNumber">driver phone number</param>
public sealed record GetDriverItem(int Id, string FirstName, string LastName, string Email, string PhoneNumber);
public sealed class GetDriversValidator : AbstractValidator<GetDriversRequest>
{
    public GetDriversValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("Page Index must be greater than 0.");
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page Size must be greater than 0.");

        RuleFor(x => x.Keyword)
            .MaximumLength(Constants.SearchMaxLength)
            .WithMessage("Keyword length must not exceed 100 characters.")
            .Matches(Constants.RecommendedSearchPattern)
            .WithMessage("Keyword is Invalid.")
            .When(x => !string.IsNullOrWhiteSpace(x.Keyword));
        
        RuleFor(x => x.OrderByColumn)
            .Must(x => Constants.ColumnsNames.Contains(x))
            .WithMessage("OrderBy Column is Invalid.")
            .When(x => x.OrderByColumn != null);
    }
}

public sealed class GetDriversHandler(IDriverQueryRepository repository) : IRequestHandler<GetDriversRequest, GetDriversResponse>
{

    public async Task<GetDriversResponse> Handle(GetDriversRequest request, CancellationToken cancellationToken)
    {
        var (totalCount, drivers) = await repository.GetDrivers(request.PageIndex, request.PageSize, request.Keyword, request.OrderByColumn, request.OrderBy);
        // Map the drivers to GetDriverItem records
        var driverItems = drivers.Select(driver => new GetDriverItem(driver.Id, driver.FirstName, driver.LastName, driver.Email, driver.PhoneNumber));
        return new GetDriversResponse(totalCount, driverItems.ToArray());
    }
}