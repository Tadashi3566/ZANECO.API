using ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;
using ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentCreateRequest : RequestExtension<PayrollAdjustmentCreateRequest>, IRequest<Guid>
{
    public DefaultIdType PayrollId { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; } = default!;
}

public class CreatePayrollAdjustmentRequestValidator : CustomValidator<PayrollAdjustmentCreateRequest>
{
    public CreatePayrollAdjustmentRequestValidator()
    {
        RuleFor(p => p.PayrollId)
             .NotEmpty();

        RuleFor(p => p.AdjustmentId)
            .NotEmpty();
    }
}

public class PayrollAdjustmentCreateRequestHandler : IRequestHandler<PayrollAdjustmentCreateRequest, Guid>
{
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<PayrollAdjustment> _repoPayrollAdjustment;
    private readonly IStringLocalizer<PayrollAdjustmentUpdateRequestHandler> _localizer;

    public PayrollAdjustmentCreateRequestHandler(IReadRepository<Payroll> repoPayroll, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<PayrollAdjustment> repoPayrollAdjustment, IStringLocalizer<PayrollAdjustmentUpdateRequestHandler> localizer) =>
        (_repoPayroll, _repoAdjustment, _repoPayrollAdjustment, _localizer) = (repoPayroll, repoAdjustment, repoPayrollAdjustment, localizer);

    public async Task<Guid> Handle(PayrollAdjustmentCreateRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repoPayroll.FirstOrDefaultAsync(new PayrollByIdSpec(request.PayrollId), cancellationToken);
        _ = payroll ?? throw new NotFoundException($"Payroll {request.PayrollId} not found.");

        var adjustment = await _repoAdjustment.FirstOrDefaultAsync(new AdjustmentByIdSpec(request.AdjustmentId), cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Payroll Adjustment {request.AdjustmentId} not found.");

        var existingPayrollAjustment = await _repoPayrollAdjustment.FirstOrDefaultAsync(new PayrollAdjustmentBySpec(payroll.Id, adjustment.Id));

        if (existingPayrollAjustment is not null)
        {
            throw new NotFoundException($"Payroll Adjustment {request.AdjustmentId} already exist.");
        }

        var payrollAdjustment = new PayrollAdjustment(payroll.Id, payroll.Name, adjustment.Id, adjustment.Number, adjustment.Name, request.Description, request.Notes);

        await _repoPayrollAdjustment.AddAsync(payrollAdjustment, cancellationToken);

        return payrollAdjustment.Id;
    }
}