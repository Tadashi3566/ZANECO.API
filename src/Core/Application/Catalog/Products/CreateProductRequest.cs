using ZANECO.API.Domain.Common.Events;

namespace ZANECO.API.Application.Catalog.Products;

public class CreateProductRequest : BaseRequestWithImage, IRequest<Guid>
{
    public DefaultIdType BrandId { get; set; }
    public string SKU { get; set; } = default!;
    public string Barcode { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;
    public bool IsVatable { get; set; } = default!;
    public decimal Rate { get; private set; } = default!;
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, DefaultIdType>
{
    private readonly IRepository<Product> _repository;
    private readonly IFileStorageService _file;

    public CreateProductRequestHandler(IRepository<Product> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var product = new Product(request.BrandId, request.SKU, request.Barcode, request.Name, request.Specification, request.UnitOfMeasurement, request.IsVatable, request.Rate, request.Description, request.Notes, imagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}