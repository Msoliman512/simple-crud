using Api.Common.Helpers;
using Api.Features.Drivers.Repositories.Query;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Queries;

public sealed class GetDriversRequest : IRequest<GetDriversResponse>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Keyword { get; init; }
    public string? OrderByColumn { get; init; }
    public bool? OrderBy { get; init; } 
}

public sealed record GetDriversResponse(int TotalCount, GetDriverItem[] Items);

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