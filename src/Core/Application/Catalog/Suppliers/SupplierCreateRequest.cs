namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierCreateRequest : IRequest<DefaultIdType>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Tin { get; set; } = default!;
    public string Agent { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public decimal Change { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateSupplierRequestValidator : CustomValidator<SupplierCreateRequest>
{
    public CreateSupplierRequestValidator(IReadRepository<Supplier> repository, IStringLocalizer<CreateSupplierRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new SupplierByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["supplier {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class CreateSupplierRequestHandler : IRequestHandler<SupplierCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;

    private readonly IFileStorageService _file;

    public CreateSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(SupplierCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Supplier>(request.Image, FileType.Image, cancellationToken);

        var supplier = new Supplier(request.Name, request.Address, request.Tin, request.Agent, request.PhoneNumber, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(supplier, cancellationToken);

        return supplier.Id;
    }
}