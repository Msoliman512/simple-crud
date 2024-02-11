
using Api.Features.Drivers.Repositories.Command;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;

/// <summary>
/// Represents the request for deleting a driver.
/// </summary>
public sealed class DeleteDriverRequest : IRequest<DeleteDriverResponse>
{
    /// <summary>
    /// The ID of the driver to delete.
    /// </summary>
    public required int Id { get; init; }
}

/// <summary>
/// Represents the response for deleting a driver.
/// </summary>
/// <param name="Result"> deletion operation result</param>
public sealed record DeleteDriverResponse(bool Result);

public sealed class DeleteDriverValidator : AbstractValidator<DeleteDriverRequest>
{
    public DeleteDriverValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}

public sealed class DeleteDriverHandler(IDriverCommandRepository repository) : IRequestHandler<DeleteDriverRequest, DeleteDriverResponse>
{

    public async Task<DeleteDriverResponse> Handle(DeleteDriverRequest request, CancellationToken cancellationToken)
    {
        
        var result = await repository.DeleteDriver(request.Id);
        return new DeleteDriverResponse(result);
    }
}