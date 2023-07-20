using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerGetViaDapperRequest : IRequest<EmployerDto>
{
    public DefaultIdType Id { get; set; }

    public EmployerGetViaDapperRequest(Guid id) => Id = id;
}

public class EmployerGetViaDapperRequestHandler : IRequestHandler<EmployerGetViaDapperRequest, EmployerDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<EmployerGetViaDapperRequestHandler> _localizer;

    public EmployerGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<EmployerGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployerDto> Handle(EmployerGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var employer = await _repository.QueryFirstOrDefaultAsync<Employer>(
            $"SELECT * FROM datazaneco.\"Employers\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = employer ?? throw new NotFoundException($"employer {request.Id} not found.");

        return employer.Adapt<EmployerDto>();
    }
}