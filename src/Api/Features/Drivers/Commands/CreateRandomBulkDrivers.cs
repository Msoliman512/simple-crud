using Api.Features.Drivers.Repositories.Command;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;
/// <summary>
/// Represents the request for random drivers bulk insertion.
/// </summary>
public sealed class CreateRandomBulkDriversRequest : IRequest<CreateRandomBulkDriversResponse>
{
    /// <summary>
    /// The number of drivers to create.
    /// </summary>
    public required int Count { get; init; }
}

/// <summary>
/// Represents the response for creating a random bulk of drivers.
/// </summary>
/// <param name="RowsInserted"> number of rows inserted</param>
public sealed record CreateRandomBulkDriversResponse(int RowsInserted);

public sealed class CreateRandomBulkDriversValidator : AbstractValidator<CreateRandomBulkDriversRequest>
{
    public CreateRandomBulkDriversValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .WithMessage("Count must be greater than zero.");
    }
}

public sealed class CreateRandomBulkDriversHandler(IDriverCommandRepository repository) : IRequestHandler<CreateRandomBulkDriversRequest, CreateRandomBulkDriversResponse>
{

    public async Task<CreateRandomBulkDriversResponse> Handle(CreateRandomBulkDriversRequest request, CancellationToken cancellationToken)
    {
        
        var result = await repository.SeedRandomDrivers(request.Count);
        return new CreateRandomBulkDriversResponse(result);
    }
}