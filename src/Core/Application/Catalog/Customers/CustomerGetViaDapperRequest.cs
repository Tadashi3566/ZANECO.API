using Mapster;

namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerGetViaDapperRequest : IRequest<CustomerDto>
{
    public DefaultIdType Id { get; set; }

    public CustomerGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class CustomerViaDapperGetRequestHandler : IRequestHandler<CustomerGetViaDapperRequest, CustomerDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<CustomerViaDapperGetRequestHandler> _localizer;

    public CustomerViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<CustomerViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CustomerDto> Handle(CustomerGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.QueryFirstOrDefaultAsync<Customer>(
            $"SELECT * FROM datazaneco.\"Customers\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = customer ?? throw new NotFoundException($"customer {request.Id} not found.");

        return customer.Adapt<CustomerDto>();
    }
}