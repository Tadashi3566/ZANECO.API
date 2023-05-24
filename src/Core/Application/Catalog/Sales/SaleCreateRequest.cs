namespace ZANECO.API.Application.Catalog.Sales;

public class SaleCreateRequest : IRequest<DefaultIdType>
{
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
    public ImageUploadRequest? Image { get; set; }
}

public class CreateSaleRequestValidator : CustomValidator<SaleCreateRequest>
{
    public CreateSaleRequestValidator(IReadRepository<Sale> repository, IStringLocalizer<CreateSaleRequestValidator> T)
    {
        RuleFor(p => p.OrNumber)
            .NotEmpty()
            .MustAsync(async (orNumber, ct) => await repository.FirstOrDefaultAsync(new SaleByOrNumerSpec(orNumber), ct) is null)
                .WithMessage((_, name) => T["sale {0} already Exists.", name]);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class CreateSaleRequestHandler : IRequestHandler<SaleCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Sale> _repository;

    private readonly IFileStorageService _file;

    public CreateSaleRequestHandler(IRepositoryWithEvents<Sale> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<DefaultIdType> Handle(SaleCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Sale>(request.Image, FileType.Image, cancellationToken);

        var sale = new Sale(request.CustomerId, request.OrNumber, request.Items, request.GrossSales, request.TotalVat, request.TotalDiscount, request.NetSales, request.Received, request.Change, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(sale, cancellationToken);

        return sale.Id;
    }
}