using Mapster;

namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountGetViaDapperRequest : IRequest<DiscountDto>
{
    public DefaultIdType Id { get; set; }

    public DiscountGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class DiscountViaDapperGetRequestHandler : IRequestHandler<DiscountGetViaDapperRequest, DiscountDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<DiscountViaDapperGetRequestHandler> _localizer;

    public DiscountViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<DiscountViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DiscountDto> Handle(DiscountGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var discount = await _repository.QueryFirstOrDefaultAsync<Discount>(
            $"SELECT * FROM datazaneco.\"Discounts\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = discount ?? throw new NotFoundException($"discount {request.Id} not found.");

        return discount.Adapt<DiscountDto>();
    }
}