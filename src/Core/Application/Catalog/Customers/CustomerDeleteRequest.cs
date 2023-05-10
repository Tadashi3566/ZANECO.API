using ZANECO.API.Application.Catalog.Sales;

namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public CustomerDeleteRequest(DefaultIdType id) => Id = id;
}

public class DeleteCustomerRequestHandler : IRequestHandler<CustomerDeleteRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repoCustomer;

    private readonly IReadRepository<Sale> _repoSale;
    private readonly IStringLocalizer _localizer;

    public DeleteCustomerRequestHandler(IRepositoryWithEvents<Customer> CustomerRepo, IReadRepository<Sale> repoSale, IStringLocalizer<DeleteCustomerRequestHandler> localizer) =>
        (_repoCustomer, _repoSale, _localizer) = (CustomerRepo, repoSale, localizer);

    public async Task<DefaultIdType> Handle(CustomerDeleteRequest request, CancellationToken cancellationToken)
    {
        if (await _repoSale.AnyAsync(new SaleByCustomerSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["customer cannot be deleted as it's being used."]);
        }

        var customer = await _repoCustomer.GetByIdAsync(request.Id, cancellationToken);

        _ = customer ?? throw new NotFoundException(_localizer["customer {0} Not Found."]);

        await _repoCustomer.DeleteAsync(customer, cancellationToken);

        return request.Id;
    }
}