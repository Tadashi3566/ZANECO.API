namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public float Percentage { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class UpdateDiscountRequestValidator : CustomValidator<DiscountUpdateRequest>
{
    public UpdateDiscountRequestValidator(IRepository<Discount> repository, IStringLocalizer<UpdateDiscountRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (Discount, name, ct) =>
                    await repository.FirstOrDefaultAsync(new DiscountByNameSpec(name), ct)
                        is not Discount existingDiscount || existingDiscount.Id == Discount.Id)
                .WithMessage((_, name) => T["discount {0} already Exists.", name]);
    }
}

public class UpdateDiscountRequestHandler : IRequestHandler<DiscountUpdateRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Discount> _repository;

    private readonly IStringLocalizer _localizer;
    private readonly IFileStorageService _file;

    public UpdateDiscountRequestHandler(IRepositoryWithEvents<Discount> repository, IStringLocalizer<UpdateDiscountRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(DiscountUpdateRequest request, CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = discount ?? throw new NotFoundException(_localizer["discount {0} Not Found.", request.Id]);

        discount.Update(request.Name, request.Percentage, request.Description!, request.Notes!);

        await _repository.UpdateAsync(discount, cancellationToken);

        return request.Id;
    }
}