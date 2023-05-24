namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Tin { get; set; } = default!;
    public string Agent { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateSupplierRequestValidator : CustomValidator<SupplierUpdateRequest>
{
    public UpdateSupplierRequestValidator(IRepository<Supplier> repository, IStringLocalizer<UpdateSupplierRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MustAsync(async (Supplier, name, ct) =>
                    await repository.FirstOrDefaultAsync(new SupplierByNameSpec(name), ct)
                        is not Supplier existingSupplier || existingSupplier.Id == Supplier.Id)
                .WithMessage((_, name) => T["supplier {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class UpdateSupplierRequestHandler : IRequestHandler<SupplierUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository, IStringLocalizer<UpdateSupplierRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(SupplierUpdateRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException(_localizer["supplier {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = supplier.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            supplier = supplier.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Supplier>(request.Image, FileType.Image, cancellationToken)
            : null;

        supplier.Update(request.Name, request.Address, request.Tin, request.Agent, request.PhoneNumber, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(supplier, cancellationToken);

        return request.Id;
    }
}