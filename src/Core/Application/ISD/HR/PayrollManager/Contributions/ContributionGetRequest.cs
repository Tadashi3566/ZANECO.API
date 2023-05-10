using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionGetRequest : IRequest<ContributionDto>
{
    public DefaultIdType Id { get; set; }

    public ContributionGetRequest(Guid id) => Id = id;
}

public class ContributionGetRequestHandler : IRequestHandler<ContributionGetRequest, ContributionDto>
{
    private readonly IRepository<Contribution> _repository;
    private readonly IStringLocalizer<ContributionGetRequestHandler> _localizer;

    public ContributionGetRequestHandler(IRepository<Contribution> repository, IStringLocalizer<ContributionGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ContributionDto> Handle(ContributionGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<Contribution, ContributionDto>)new ContributionByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Contribution not found."], request.Id));
}