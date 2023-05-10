namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DiscountDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteDiscountRequestHandler : IRequestHandler<DiscountDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Discount> _repoDiscount;

    private readonly IReadRepository<Sale> _repoSale;
    private readonly IStringLocalizer _localizer;

    public DeleteDiscountRequestHandler(IRepositoryWithEvents<Discount> DiscountRepo, IReadRepository<Sale> repoSale, IStringLocalizer<DeleteDiscountRequestHandler> localizer) =>
        (_repoDiscount, _repoSale, _localizer) = (DiscountRepo, repoSale, localizer);

    public async Task<DefaultIdType> Handle(DiscountDeleteRequest request, CancellationToken cancellationToken)
    {
        //if (await _repoSaleItem.AnyAsync(new DiscountByIdSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_localizer["discount cannot be deleted as it's being used."]);
        //}

        var discount = await _repoDiscount.GetByIdAsync(request.Id, cancellationToken);

        _ = discount ?? throw new NotFoundException(_localizer["discount {0} Not Found."]);

        await _repoDiscount.DeleteAsync(discount, cancellationToken);

        return request.Id;
    }
}