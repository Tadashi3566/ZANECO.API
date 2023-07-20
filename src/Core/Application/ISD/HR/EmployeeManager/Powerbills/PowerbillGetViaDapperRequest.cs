using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillGetViaDapperRequest : IRequest<PowerbillDto>
{
    public DefaultIdType Id { get; set; }

    public PowerbillGetViaDapperRequest(Guid id) => Id = id;
}

public class PowerbillGetViaDapperRequestHandler : IRequestHandler<PowerbillGetViaDapperRequest, PowerbillDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PowerbillGetViaDapperRequestHandler> _localizer;

    public PowerbillGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<PowerbillGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerbillDto> Handle(PowerbillGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var powerbill = await _repository.QueryFirstOrDefaultAsync<Powerbill>(
            $"SELECT * FROM datazaneco.\"Powerbills\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = powerbill ?? throw new NotFoundException($"powerbill {request.Id} not found.");

        return powerbill.Adapt<PowerbillDto>();
    }
}