using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionSearchRequest : PaginationFilter, IRequest<PaginationResponse<PowerConsumptionDto>>
{
}

public class PowerConsumptionsBySearchRequestSpec : EntitiesByPaginationFilterSpec<PowerConsumption, PowerConsumptionDto>
{
    public PowerConsumptionsBySearchRequestSpec(PowerConsumptionSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.GroupName, !request.HasOrderBy());
}

public class PowerConsumptionSearchRequestHandler : IRequestHandler<PowerConsumptionSearchRequest, PaginationResponse<PowerConsumptionDto>>
{
    private readonly IReadRepository<PowerConsumption> _repository;

    public PowerConsumptionSearchRequestHandler(IReadRepository<PowerConsumption> repository) => _repository = repository;

    public async Task<PaginationResponse<PowerConsumptionDto>> Handle(PowerConsumptionSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PowerConsumptionsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}