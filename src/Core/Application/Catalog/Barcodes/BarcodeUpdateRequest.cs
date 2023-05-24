namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateBarcodeRequestValidator : CustomValidator<BarcodeUpdateRequest>
{
    public UpdateBarcodeRequestValidator(IRepository<Barcode> repository, IStringLocalizer<UpdateBarcodeRequestValidator> T)
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (Barcode, code, ct) =>
                    await repository.FirstOrDefaultAsync(new BarcodeByCodeSpec(code), ct)
                        is not Barcode existingBarcode || existingBarcode.Id == Barcode.Id)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (Barcode, name, ct) =>
                    await repository.FirstOrDefaultAsync(new BarcodeByNameSpec(name), ct)
                        is not Barcode existingBarcode || existingBarcode.Id == Barcode.Id)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Specification)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (Barcode, specification, ct) =>
                    await repository.FirstOrDefaultAsync(new BarcodeBySpecificationSpec(specification), ct)
                        is not Barcode existingBarcode || existingBarcode.Id == Barcode.Id)
                .WithMessage((_, name) => T["barcode {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class UpdateBarcodeRequestHandler : IRequestHandler<BarcodeUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Barcode> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateBarcodeRequestHandler(IRepositoryWithEvents<Barcode> repository, IStringLocalizer<UpdateBarcodeRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(BarcodeUpdateRequest request, CancellationToken cancellationToken)
    {
        var barcode = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = barcode ?? throw new NotFoundException(_localizer["barcode {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = barcode.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            barcode = barcode.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Barcode>(request.Image, FileType.Image, cancellationToken)
            : null;

        barcode.Update(request.Code, request.Name, request.Specification, request.UnitOfMeasurement, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(barcode, cancellationToken);

        return request.Id;
    }
}