using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentGetViaDapperRequest : IRequest<DependentDto>
{
    public DefaultIdType Id { get; set; }

    public DependentGetViaDapperRequest(Guid id) => Id = id;
}

public class DependentGetViaDapperRequestHandler : IRequestHandler<DependentGetViaDapperRequest, DependentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<DependentGetViaDapperRequestHandler> _localizer;

    public DependentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<DependentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DependentDto> Handle(DependentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var dependent = await _repository.QueryFirstOrDefaultAsync<Dependent>(
        $"SELECT * FROM datazaneco.\"Dependents\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = dependent ?? throw new NotFoundException($"dependent {request.Id} not found.");

        return dependent.Adapt<DependentDto>();
    }
}