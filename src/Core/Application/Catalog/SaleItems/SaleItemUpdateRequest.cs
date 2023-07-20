namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
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
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateSaleItemRequestValidator : CustomValidator<SaleItemUpdateRequest>
{
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class UpdateSaleItemRequestHandler : IRequestHandler<SaleItemUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<SaleItem> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateSaleItemRequestHandler(IRepositoryWithEvents<SaleItem> repository, IStringLocalizer<UpdateSaleItemRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(SaleItemUpdateRequest request, CancellationToken cancellationToken)
    {
        var saleItem = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = saleItem ?? throw new NotFoundException($"saleItem {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = saleItem.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            saleItem = saleItem.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<SaleItem>(request.Image, FileType.Image, cancellationToken)
            : null;

        saleItem.Update(request.BarcodeId, request.DiscountId, request.Items, request.Name, request.Gross, request.Vat, request.DiscountAmount, request.Net, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(saleItem, cancellationToken);

        return request.Id;
    }
}