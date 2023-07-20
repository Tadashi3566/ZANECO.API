using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationGetViaDapperRequest : IRequest<DesignationDto>
{
    public DefaultIdType Id { get; set; }

    public DesignationGetViaDapperRequest(Guid id) => Id = id;
}

public class DesignationGetViaDapperRequestHandler : IRequestHandler<DesignationGetViaDapperRequest, DesignationDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<DesignationGetViaDapperRequestHandler> _localizer;

    public DesignationGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<DesignationGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DesignationDto> Handle(DesignationGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var designation = await _repository.QueryFirstOrDefaultAsync<Designation>(
        $"SELECT * FROM datazaneco.\"Designations\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = designation ?? throw new NotFoundException($"designation {request.Id} not found.");

        return designation.Adapt<DesignationDto>();
    }
}