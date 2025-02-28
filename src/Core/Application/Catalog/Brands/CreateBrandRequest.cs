namespace ZANECO.API.Application.Catalog.Brands;

public class CreateBrandRequest : IRequest<DefaultIdType>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateBrandRequestValidator : CustomValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator(IReadRepository<Brand> repository, IStringLocalizer<CreateBrandRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
}

public class CreateBrandRequestHandler : IRequestHandler<CreateBrandRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Brand> _repository;

    public CreateBrandRequestHandler(IRepositoryWithEvents<Brand> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = new Brand(request.Name, request.Description, request.Notes);

        await _repository.AddAsync(brand, cancellationToken);

        return brand.Id;
    }
}