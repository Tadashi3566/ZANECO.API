namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountGetRequest : IRequest<DiscountDto>
{
    public DefaultIdType Id { get; set; }

    public DiscountGetRequest(DefaultIdType id) => Id = id;
}

public class GetDiscountRequestHandler : IRequestHandler<DiscountGetRequest, DiscountDto>
{
    private readonly IRepository<Discount> _repository;
    private readonly IStringLocalizer _localizer;

    public GetDiscountRequestHandler(IRepository<Discount> repository, IStringLocalizer<GetDiscountRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<DiscountDto> Handle(DiscountGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Discount, DiscountDto>)new DiscountByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_localizer["Discount {0} Not Found.", request.Id]);
}