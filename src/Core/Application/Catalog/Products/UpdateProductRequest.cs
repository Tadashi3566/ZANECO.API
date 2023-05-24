using ZANECO.API.Domain.Common.Events;

namespace ZANECO.API.Application.Catalog.Products;

public class UpdateProductRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType BrandId { get; set; }
    public string SKU { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;
    public bool IsVatable { get; set; } = default!;
    public decimal Rate { get; set; } = default!;
    public string Description { get; private set; } = default!;
    public string Notes { get; private set; } = default!;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateProductRequestHandler(IRepository<Product> repository, IStringLocalizer<UpdateProductRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Product {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentProductImagePath = product.ImagePath;
            if (!string.IsNullOrEmpty(currentProductImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentProductImagePath));
            }

            product = product.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedProduct = product.Update(request.BrandId, request.SKU, request.Barcode, request.Name, request.Specification, request.UnitOfMeasurement, request.IsVatable, request.Rate, request.Description, request.Notes, imagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityUpdatedEvent.WithEntity(product));

        await _repository.UpdateAsync(updatedProduct, cancellationToken);

        return request.Id;
    }
}