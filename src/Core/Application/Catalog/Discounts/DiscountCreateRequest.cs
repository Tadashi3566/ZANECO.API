namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountCreateRequest : IRequest<DefaultIdType>
{
    public string Name { get; set; } = default!;
    public float Percentage { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateDiscountRequestValidator : CustomValidator<DiscountCreateRequest>
{
    public CreateDiscountRequestValidator(IReadRepository<Discount> repository, IStringLocalizer<CreateDiscountRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new DiscountByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["discount {0} already Exists.", name]);
    }
}

public class CreateDiscountRequestHandler : IRequestHandler<DiscountCreateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Discount> _repository;

    public CreateDiscountRequestHandler(IRepositoryWithEvents<Discount> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(DiscountCreateRequest request, CancellationToken cancellationToken)
    {
        var discount = new Discount(request.Name, request.Percentage, request.Description!, request.Notes!);

        await _repository.AddAsync(discount, cancellationToken);

        return discount.Id;
    }
}