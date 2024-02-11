
using Api.Features.Drivers.Repositories.Command;
using FluentValidation;
using MediatR;

namespace Api.Features.Drivers.Commands;

public sealed class DeleteDriverRequest : IRequest<DeleteDriverResponse>
{
    public required int Id { get; init; }
}

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