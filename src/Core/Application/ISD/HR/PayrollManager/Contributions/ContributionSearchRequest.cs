using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionSearchRequest : PaginationFilter, IRequest<PaginationResponse<ContributionDto>>
{
}

public class ContributionBySearchRequestSpec : EntitiesByPaginationFilterSpec<Contribution, ContributionDto>
{
    public ContributionBySearchRequestSpec(ContributionSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.ContributionType, !request.HasOrderBy());
}

public class ContributionSearchRequestHandler : IRequestHandler<ContributionSearchRequest, PaginationResponse<ContributionDto>>
{
    private readonly IReadRepository<Contribution> _repository;

    public ContributionSearchRequestHandler(IReadRepository<Contribution> repository) => _repository = repository;

    public async Task<PaginationResponse<ContributionDto>> Handle(ContributionSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new ContributionBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}