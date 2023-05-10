namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerSearchRequest : PaginationFilter, IRequest<PaginationResponse<CustomerDto>>
{
}

public class CustomersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Customer, CustomerDto>
{
    public CustomersBySearchRequestSpec(CustomerSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchCustomersRequestHandler : IRequestHandler<CustomerSearchRequest, PaginationResponse<CustomerDto>>
{
    private readonly IReadRepository<Customer> _repository;

    public SearchCustomersRequestHandler(IReadRepository<Customer> repository) => _repository = repository;

    public async Task<PaginationResponse<CustomerDto>> Handle(CustomerSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}