using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryGetViaDapperRequest : IRequest<SalaryDto>
{
    public DefaultIdType Id { get; set; }

    public SalaryGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class SalaryGetViaDapperRequestHandler : IRequestHandler<SalaryGetViaDapperRequest, SalaryDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<SalaryGetViaDapperRequestHandler> _localizer;

    public SalaryGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<SalaryGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SalaryDto> Handle(SalaryGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var salary = await _repository.QueryFirstOrDefaultAsync<Salary>(
            $"SELECT * FROM datazaneco.\"Salaries\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = salary ?? throw new NotFoundException(string.Format(_localizer["salary not found."], request.Id));

        return salary.Adapt<SalaryDto>();
    }
}