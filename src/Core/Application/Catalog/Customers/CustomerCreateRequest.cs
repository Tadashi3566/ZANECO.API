namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerCreateRequest : IRequest<DefaultIdType>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public decimal Investment { get; set; } = default!;
    public decimal Sales { get; set; } = default!;
    public int Points { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateCustomerRequestValidator : CustomValidator<CustomerCreateRequest>
{
    public CreateCustomerRequestValidator(IReadRepository<Customer> repository, IStringLocalizer<CreateCustomerRequestValidator> T)
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (code, ct) => await repository.FirstOrDefaultAsync(new CustomerByCodeSpec(code), ct) is null)
                .WithMessage((_, name) => T["customer {0} already Exists.", name]);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new CustomerByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["customer {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class CreateCustomerRequestHandler : IRequestHandler<CustomerCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repository;

    private readonly IFileStorageService _file;

    public CreateCustomerRequestHandler(IRepositoryWithEvents<Customer> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(CustomerCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Customer>(request.Image, FileType.Image, cancellationToken);

        var customer = new Customer(request.Code, request.Name, request.Address, request.PhoneNumber, request.Investment, request.Sales, request.Points, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(customer, cancellationToken);

        return customer.Id;
    }
}