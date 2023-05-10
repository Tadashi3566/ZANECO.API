using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateSearchRequest : PaginationFilter, IRequest<PaginationResponse<PowerRateDto>>
{
}

public class PowerRatesBySearchRequestSpec : EntitiesByPaginationFilterSpec<PowerRate, PowerRateDto>
{
    public PowerRatesBySearchRequestSpec(PowerRateSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Code, !request.HasOrderBy());
}

public class PowerRateSearchRequestHandler : IRequestHandler<PowerRateSearchRequest, PaginationResponse<PowerRateDto>>
{
    private readonly IReadRepository<PowerRate> _repository;

    public PowerRateSearchRequestHandler(IReadRepository<PowerRate> repository) => _repository = repository;

    public async Task<PaginationResponse<PowerRateDto>> Handle(PowerRateSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PowerRatesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}