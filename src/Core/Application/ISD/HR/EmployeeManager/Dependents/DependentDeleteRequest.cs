using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public DependentDeleteRequest(Guid id) => Id = id;
}

public class DependentDeleteRequestHandler : IRequestHandler<DependentDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Dependent> _repository;
    private readonly IStringLocalizer<DependentDeleteRequestHandler> _localizer;

    public DependentDeleteRequestHandler(IRepositoryWithEvents<Dependent> repository, IStringLocalizer<DependentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DependentDeleteRequest request, CancellationToken cancellationToken)
    {
        var dependent = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = dependent ?? throw new NotFoundException($"dependent {request.Id} not found.");

        await _repository.DeleteAsync(dependent, cancellationToken);

        return request.Id;
    }
}