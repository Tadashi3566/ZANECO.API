namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerGetRequest : IRequest<CustomerDto>
{
    public DefaultIdType Id { get; set; }

    public CustomerGetRequest(DefaultIdType id) => Id = id;
}

public class GetCustomerRequestHandler : IRequestHandler<CustomerGetRequest, CustomerDto>
{
    private readonly IRepository<Customer> _repository;
    private readonly IStringLocalizer _localizer;

    public GetCustomerRequestHandler(IRepository<Customer> repository, IStringLocalizer<GetCustomerRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<CustomerDto> Handle(CustomerGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Customer, CustomerDto>)new CustomerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_localizer["Customer {0} Not Found.", request.Id]);
}