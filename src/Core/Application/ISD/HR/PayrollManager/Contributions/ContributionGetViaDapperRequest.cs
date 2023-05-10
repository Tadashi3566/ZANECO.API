using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionGetViaDapperRequest : IRequest<ContributionDto>
{
    public DefaultIdType Id { get; set; }

    public ContributionGetViaDapperRequest(Guid id) => Id = id;
}

public class ContributionGetViaDapperRequestHandler : IRequestHandler<ContributionGetViaDapperRequest, ContributionDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<ContributionGetViaDapperRequestHandler> _localizer;

    public ContributionGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<ContributionGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ContributionDto> Handle(ContributionGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var contribution = await _repository.QueryFirstOrDefaultAsync<Contribution>(
        $"SELECT * FROM datazaneco.\"Contribution\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = contribution ?? throw new NotFoundException(string.Format(_localizer["Contribution not found."], request.Id));

        return contribution.Adapt<ContributionDto>();
    }
}