namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType SaleId { get; private set; }
    public DefaultIdType ProductId { get; private set; }
    public DefaultIdType BarcodeId { get; private set; }
    public DefaultIdType DiscountId { get; private set; }
    public DateTime Date { get; set; } = default!;
    public string Transaction { get; set; } = default!;
    public int Items { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Gross { get; set; } = default!;
    public decimal Vat { get; set; } = default!;
    public decimal DiscountAmount { get; set; } = default!;
    public decimal Net { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateSaleItemRequestValidator : CustomValidator<SaleItemCreateRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class CreateSaleItemRequestHandler : IRequestHandler<SaleItemCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SaleItem> _repository;

    private readonly IFileStorageService _file;

    public CreateSaleItemRequestHandler(IRepositoryWithEvents<SaleItem> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(SaleItemCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<SaleItem>(request.Image, FileType.Image, cancellationToken);

        var saleItem = new SaleItem(request.SaleId, request.ProductId, request.BarcodeId, request.DiscountId, request.Items, request.Name, request.Gross, request.Vat, request.DiscountAmount, request.Net, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(saleItem, cancellationToken);

        return saleItem.Id;
    }
}