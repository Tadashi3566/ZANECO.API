namespace ZANECO.API.Application.Catalog.Sales;

public class SaleUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType CustomerId { get; set; }
    public double OrNumber { get; set; } = default!;
    public int Items { get; set; } = default!;
    public decimal GrossSales { get; set; } = default!;
    public decimal TotalVat { get; set; } = default!;
    public decimal TotalDiscount { get; set; } = default!;
    public decimal NetSales { get; set; } = default!;
    public decimal Received { get; set; } = default!;
    public decimal Change { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class UpdateSaleRequestValidator : CustomValidator<SaleUpdateRequest>
{
    public UpdateSaleRequestValidator(IRepository<Sale> repository, IStringLocalizer<UpdateSaleRequestValidator> T)
    {
        RuleFor(p => p.OrNumber)
            .NotEmpty()
            .MustAsync(async (Sale, orNumber, ct) =>
                    await repository.FirstOrDefaultAsync(new SaleByOrNumerSpec(orNumber), ct)
                        is not { } existingSale || existingSale.Id == Sale.Id)
                .WithMessage((_, orNumer) => T["sale {0} already Exists.", orNumer]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class UpdateSaleRequestHandler : IRequestHandler<SaleUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sale> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateSaleRequestHandler(IRepositoryWithEvents<Sale> repository, IStringLocalizer<UpdateSaleRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(SaleUpdateRequest request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = sale ?? throw new NotFoundException($"sale {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = sale.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            sale = sale.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Sale>(request.Image, FileType.Image, cancellationToken)
            : null;

        sale.Update(request.CustomerId, request.OrNumber, request.Items, request.GrossSales, request.TotalVat, request.TotalDiscount, request.NetSales, request.Received, request.Change, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(sale, cancellationToken);

        return request.Id;
    }
}