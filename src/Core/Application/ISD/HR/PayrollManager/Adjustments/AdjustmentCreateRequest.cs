using ZANECO.API.Application.App.Groups;
using ZANECO.API.Domain.App;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentCreateRequest : IRequest<Guid>
{
    public string AdjustmentType { get; set; } = default!;
    public string EmployeeType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public bool IsOptional { get; set; }
    public bool IsLoan { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateAdjustmentRequestValidator : CustomValidator<AdjustmentCreateRequest>
{
    public CreateAdjustmentRequestValidator(IReadRepository<Adjustment> repoAdjustment, IStringLocalizer<CreateAdjustmentRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeType)
            .NotEmpty();

        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (number, ct) => await repoAdjustment.FirstOrDefaultAsync(new AdjustmentByNumberSpec(number), ct) is null)
            .WithMessage((_, number) => string.Format(localizer["adjustment already exists"], number));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(32)
            .MustAsync(async (name, ct) => await repoAdjustment.FirstOrDefaultAsync(new AdjustmentByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["adjustment already exists"], name));

        RuleFor(p => p.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

public class AdjustmentCreateRequestHandler : IRequestHandler<AdjustmentCreateRequest, Guid>
{
    private readonly IReadRepository<Group> _repoGroup;
    private readonly IRepositoryWithEvents<Adjustment> _repoAdjustment;

    public AdjustmentCreateRequestHandler(IReadRepository<Group> repoGroup, IRepositoryWithEvents<Adjustment> repoAdjsutment) =>
        (_repoGroup, _repoAdjustment) = (repoGroup, repoAdjsutment);

    public async Task<Guid> Handle(AdjustmentCreateRequest request, CancellationToken cancellationToken)
    {
        var group = await _repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(request.Name), cancellationToken);

        decimal amount = request.Amount.Equals(0) ? group!.Amount : request.Amount;

        var adjustment = new Adjustment(group!.Id, group.Tag, request.EmployeeType, request.Number, request.Name, amount, request.PaymentSchedule, request.IsOptional, request.IsLoan, request.IsActive, request.Description, request.Notes);

        await _repoAdjustment.AddAsync(adjustment, cancellationToken);

        return adjustment.Id;
    }
}