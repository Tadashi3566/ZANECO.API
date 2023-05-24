using ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;
using ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType PayrollId { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class PayrollAdjustmentUpdateRequestValidator : CustomValidator<PayrollAdjustmentUpdateRequest>
{
    public PayrollAdjustmentUpdateRequestValidator()
    {
        RuleFor(p => p.PayrollId)
             .NotEmpty();

        RuleFor(p => p.AdjustmentId)
            .NotEmpty();
    }
}

public class PayrollAdjustmentUpdateRequestHandler : IRequestHandler<PayrollAdjustmentUpdateRequest, Guid>
{
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<PayrollAdjustment> _repoPayrollAdjustment;
    private readonly IStringLocalizer<PayrollAdjustmentUpdateRequestHandler> _localizer;

    public PayrollAdjustmentUpdateRequestHandler(IReadRepository<Payroll> repoPayroll, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<PayrollAdjustment> repoAdjsutment, IStringLocalizer<PayrollAdjustmentUpdateRequestHandler> localizer) =>
        (_repoPayroll, _repoAdjustment, _repoPayrollAdjustment, _localizer) = (repoPayroll, repoAdjustment, repoAdjsutment, localizer);

    public async Task<Guid> Handle(PayrollAdjustmentUpdateRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repoPayroll.FirstOrDefaultAsync(new PayrollByIdSpec(request.PayrollId), cancellationToken);
        _ = payroll ?? throw new NotFoundException(string.Format(_localizer["Payroll not found."], request.Id));

        var adjustment = await _repoAdjustment.FirstOrDefaultAsync(new AdjustmentByIdSpec(request.AdjustmentId), cancellationToken);
        _ = adjustment ?? throw new NotFoundException(string.Format(_localizer["Adjustment not found."], request.Id));

        var payrollAdjustment = await _repoPayrollAdjustment.GetByIdAsync(request.Id, cancellationToken);
        _ = payrollAdjustment ?? throw new NotFoundException(string.Format(_localizer["Payroll Adjustment not found."], request.Id));

        var updatedPayrollAdjustment = payrollAdjustment.Update(payroll.Name, adjustment.Number, adjustment.Name, request.Description, request.Notes);
        await _repoPayrollAdjustment.UpdateAsync(updatedPayrollAdjustment, cancellationToken);

        return request.Id;
    }
}