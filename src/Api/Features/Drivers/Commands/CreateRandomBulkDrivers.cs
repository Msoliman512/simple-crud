using Api.Features.Drivers.Repositories.Command;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;

public sealed class CreateRandomBulkDriversRequest : IRequest<CreateRandomBulkDriversResponse>
{
    public required int Count { get; init; }
}

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