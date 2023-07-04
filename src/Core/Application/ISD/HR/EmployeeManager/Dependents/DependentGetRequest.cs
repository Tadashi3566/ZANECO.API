using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentGetRequest : IRequest<DependentDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public DependentGetRequest(Guid id) => Id = id;
}

public class DependentGetRequestHandler : IRequestHandler<DependentGetRequest, DependentDetailsDto>
{
    private readonly IRepository<Dependent> _repository;
    private readonly IStringLocalizer<DependentGetRequestHandler> _localizer;

    public DependentGetRequestHandler(IRepository<Dependent> repository, IStringLocalizer<DependentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DependentDetailsDto> Handle(DependentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new DependentByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["dependent not found."], request.Id));
}