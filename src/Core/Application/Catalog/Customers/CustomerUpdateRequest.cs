namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public decimal Investment { get; set; } = default!;
    public decimal Sales { get; set; } = default!;
    public int Points { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateCustomerRequestValidator : CustomValidator<CustomerUpdateRequest>
{
    public UpdateCustomerRequestValidator(IRepository<Customer> repository, IStringLocalizer<UpdateCustomerRequestValidator> T)
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (Customer, code, ct) =>
                    await repository.FirstOrDefaultAsync(new CustomerByCodeSpec(code), ct)
                        is not { } existingCustomer || existingCustomer.Id == Customer.Id)
                .WithMessage((_, name) => T["customer {0} already Exists.", name]);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (Customer, name, ct) =>
                    await repository.FirstOrDefaultAsync(new CustomerByNameSpec(name), ct)
                        is not { } existingCustomer || existingCustomer.Id == Customer.Id)
                .WithMessage((_, name) => T["customer {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class UpdateCustomerRequestHandler : IRequestHandler<CustomerUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateCustomerRequestHandler(IRepositoryWithEvents<Customer> repository, IStringLocalizer<UpdateCustomerRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = customer ?? throw new NotFoundException(_localizer["customer {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = customer.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            customer = customer.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Customer>(request.Image, FileType.Image, cancellationToken)
            : null;

        customer.Update(request.Code, request.Name, request.Address, request.PhoneNumber, request.Investment, request.Sales, request.Points, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(customer, cancellationToken);

        return request.Id;
    }
}