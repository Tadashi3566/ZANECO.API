using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollGenerateRequest : IRequest<bool>
{
    public DefaultIdType Id { get; set; }
}

public class PayrollGenerateRequestValidator : CustomValidator<PayrollGenerateRequest>
{
    public PayrollGenerateRequestValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
    }
}

public class PayrollGenerateRequestHandler : IRequestHandler<PayrollGenerateRequest, bool>
{
    private readonly IReadRepository<EmployeePayrollDetail> _repoPayrollDetails;
    private readonly IRepositoryWithEvents<Payroll> _repoPayroll;
    private readonly IStringLocalizer<PayrollGenerateRequestHandler> _localizer;

    public PayrollGenerateRequestHandler(IReadRepository<EmployeePayrollDetail> repoPayrollDetail, IRepositoryWithEvents<Payroll> repoPayroll, IStringLocalizer<PayrollGenerateRequestHandler> localizer) =>
        (_repoPayrollDetails, _repoPayroll, _localizer) = (repoPayrollDetail, repoPayroll, localizer);

    public async Task<bool> Handle(PayrollGenerateRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repoPayroll.GetByIdAsync(request.Id, cancellationToken);
        _ = payroll ?? throw new NotFoundException(string.Format(_localizer["payroll not found."], request.Id));

        if (payroll is not null)
        {
            var payrollDetails = await _repoPayrollDetails.ListAsync(new PayrollByPayrollDetailSpec(request.Id), cancellationToken);

            decimal totalSalary = payrollDetails
                .Where(x => x.AdjustmentName.Equals("BASIC PAY"))
                .Select(x => x.Amount).Sum();

            decimal totalAdditional = payrollDetails
                .Where(x => x.AdjustmentType.Equals("ADDITIONAL") && !x.AdjustmentName.Equals("BASIC PAY"))
                .Select(x => x.Amount).Sum();

            decimal totalDeduction = payrollDetails
                .Where(x => x.AdjustmentType.Equals("DEDUCTION") && x.Contributor.Equals("EMPLOYEE"))
                .Select(x => x.Amount).Sum();

            var updatedPayroll = payroll.Update(payroll.PayrollType, payroll.EmploymentType, payroll.Name, totalSalary, totalAdditional, totalSalary + totalAdditional, totalDeduction, (totalSalary + totalAdditional) - totalDeduction, payroll.StartDate, payroll.EndDate, payroll.WorkingDays, payroll.PayrollDate, payroll.IsClosed);

            await _repoPayroll.UpdateAsync(updatedPayroll, cancellationToken);

            return true;
        }

        return false;
    }
}