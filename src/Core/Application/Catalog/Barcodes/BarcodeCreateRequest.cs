namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType ProductId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateBarcodeRequestValidator : CustomValidator<BarcodeCreateRequest>
{
    public CreateBarcodeRequestValidator(IReadRepository<Barcode> repository, IStringLocalizer<CreateBarcodeRequestValidator> T)
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (code, ct) => await repository.FirstOrDefaultAsync(new BarcodeByCodeSpec(code), ct) is null)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BarcodeByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Specification)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (specification, ct) => await repository.FirstOrDefaultAsync(new BarcodeBySpecificationSpec(specification), ct) is null)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class CreateBarcodeRequestHandler : IRequestHandler<BarcodeCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Barcode> _repository;

    private readonly IFileStorageService _file;

    public CreateBarcodeRequestHandler(IRepositoryWithEvents<Barcode> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(BarcodeCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Barcode>(request.Image, FileType.Image, cancellationToken);

        var barcode = new Barcode(request.ProductId, request.Code, request.Name, request.Specification, request.UnitOfMeasurement, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(barcode, cancellationToken);

        return barcode.Id;
    }
}