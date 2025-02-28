using ZANECO.API.Application.App.Groups;
using ZANECO.API.Domain.App;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentUpdateRequest : BaseRequest, IRequest<Guid>
{
    public string AdjustmentType { get; set; } = default!;
    public string EmployeeType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public bool IsOptional { get; set; }
    public bool IsLoan { get; set; }
    public bool IsActive { get; set; }
}

public class AdjustmentUpdateRequestValidator : CustomValidator<AdjustmentUpdateRequest>
{
    public AdjustmentUpdateRequestValidator(IReadRepository<Adjustment> repoAdjustment, IStringLocalizer<AdjustmentUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeType)
            .NotEmpty();

        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (adjustment, number, ct) => await repoAdjustment.FirstOrDefaultAsync(new AdjustmentByNumberSpec(number), ct)
                        is not { } existingAdjustment || existingAdjustment.Id == adjustment.Id)
            .WithMessage((_, number) => string.Format(localizer["adjustment already exists."], number));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(32)
            .MustAsync(async (adjustment, name, ct) => await repoAdjustment.FirstOrDefaultAsync(new AdjustmentByNameSpec(name), ct)
                        is not { } existingAdjustment || existingAdjustment.Id == adjustment.Id)
            .WithMessage((_, name) => string.Format(localizer["adjustment already exists."], name));

        RuleFor(p => p.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

public class AdjustmentUpdateRequestHandler : IRequestHandler<AdjustmentUpdateRequest, Guid>
{
    private readonly IReadRepository<Group> _repoGroup;
    private readonly IRepositoryWithEvents<Adjustment> _repoAdjustment;

    public AdjustmentUpdateRequestHandler(IReadRepository<Group> repoGroup, IRepositoryWithEvents<Adjustment> repoAdjsutment) =>
        (_repoGroup, _repoAdjustment) = (repoGroup, repoAdjsutment);

    public async Task<Guid> Handle(AdjustmentUpdateRequest request, CancellationToken cancellationToken)
    {
        var group = await _repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(request.Name), cancellationToken);

        var adjustment = await _repoAdjustment.GetByIdAsync(request.Id, cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.Id} not found.");

        decimal amount = request.Amount.Equals(0) ? group!.Amount : request.Amount;

        var updatedAdjustment = adjustment.Update(group!.Id, group.Tag, request.EmployeeType, request.Number, request.Name, amount, request.PaymentSchedule, request.IsOptional, request.IsLoan, request.IsActive, request.Description, request.Notes);
        await _repoAdjustment.UpdateAsync(updatedAdjustment, cancellationToken);

        return request.Id;
    }
}