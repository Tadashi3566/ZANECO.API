using Mapster;
using ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;
using ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;
using ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;
using ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;
using ZANECO.API.Application.ISD.HR.PayrollManager.Loans;
using ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailGenerateRequest : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid PayrollId { get; set; }
}

public class EmployeePayrollDetailGenerateRequestValidator : CustomValidator<EmployeePayrollDetailGenerateRequest>
{
    public EmployeePayrollDetailGenerateRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();

        RuleFor(p => p.PayrollId)
            .NotEmpty();
    }
}

public class EmployeePayrollDetailGenerateRequestHandler : IRequestHandler<EmployeePayrollDetailGenerateRequest, bool>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Payroll> _repoPayroll;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IReadRepository<PayrollAdjustment> _repoPayrollAdjustment;
    private readonly IReadRepository<Loan> _repoEmployeeLoan;
    private readonly IReadRepository<Contribution> _repoContribution;
    private readonly IReadRepository<EmployeeAdjustment> _repoEmployeeAdjustment;
    private readonly IRepositoryWithEvents<EmployeePayroll> _repoEmployeePayroll;
    private readonly IRepositoryWithEvents<EmployeePayrollDetail> _repoEmployeePayrollDetail;
    private readonly IStringLocalizer<AdjustmentUpdateRequestHandler> _localizer;

    public EmployeePayrollDetailGenerateRequestHandler(
        IReadRepository<Employee> repoEmployee,
        IReadRepository<Payroll> repoPayroll,
        IReadRepository<Adjustment> repoAdjustment,
        IReadRepository<PayrollAdjustment> repoPayrollAdjustment,
        IReadRepository<Loan> repoEmployeeLoan,
        IReadRepository<Contribution> repoContribution,
        IReadRepository<EmployeeAdjustment> repoEmployeeAdjustment,
        IRepositoryWithEvents<EmployeePayroll> repoEmployeePayroll,
        IRepositoryWithEvents<EmployeePayrollDetail> repoEmployeePayrollDetail,
        IStringLocalizer<AdjustmentUpdateRequestHandler> localizer) =>
        (_repoEmployee, _repoPayroll, _repoAdjustment, _repoPayrollAdjustment, _repoEmployeeLoan, _repoContribution, _repoEmployeeAdjustment, _repoEmployeePayroll, _repoEmployeePayrollDetail, _localizer) =
        (repoEmployee, repoPayroll, repoAdjustment, repoPayrollAdjustment, repoEmployeeLoan, repoContribution, repoEmployeeAdjustment, repoEmployeePayroll, repoEmployeePayrollDetail, localizer);

    public async Task<bool> Handle(EmployeePayrollDetailGenerateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");
        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        // Get Payroll Information
        var payroll = await _repoPayroll.GetByIdAsync(request.PayrollId, cancellationToken);
        _ = payroll ?? throw new NotFoundException($"Payroll {request.PayrollId} not found.");
        if (payroll.IsClosed) return false;

        string payrollType = payroll.PayrollType;

        // Payroll will generate only for the same Employment Types i.e. Payroll for Regular or Job Order
        if (!payroll.EmploymentType.Equals(employee.EmploymentType))
            return false;

        // Get Adjustments for all types of employees
        var adjustmentsAll = _repoPayrollAdjustment.ListAsync(new PayrollAdjustmentByPayrollSpec(payroll.Id), cancellationToken);

        decimal amount = 0;
        decimal salary = 0;
        int divisor = 0;

        foreach (var payrollAdjustment in await adjustmentsAll)
        {
            var adjustment = await _repoAdjustment.GetByIdAsync(payrollAdjustment.AdjustmentId, cancellationToken);
            _ = adjustment ?? throw new NotFoundException($"Adjustment {payrollAdjustment.AdjustmentId} not found.");

            // Assign Divisor for Every Payroll or Monthly
            divisor = adjustment.PaymentSchedule.Equals("PAYROLL") ? 2 : 1;

            // Set salary according to Employment Type;
            salary = employee.EmploymentType.Equals("REGULAR") ? employee.SalaryAmount / divisor : employee.RatePerDay * payroll.WorkingDays;

            // Priority (Every)Payroll Adjustments
            //if ((adjustment.PaymentSchedule.Equals("PAYROLL") || adjustment.PaymentSchedule.Equals(payrollType)) && (!adjustment.IsOptional || adjustment.IsLoan))
            //{
            if (adjustment.Name.Equals("BASIC PAY"))
            {
                amount = salary;
            }
            else if (adjustment.Name.Equals("PAG-IBIG"))
            {
                amount = salary * 0.02M / divisor;

                // Employer and Employee contributions are with the same amount
                await SavePayrollDetail(employee, payroll, adjustment.Adapt<AdjustmentDto>(), amount, "COMPANY", cancellationToken);
            }
            else if (adjustment.Name.Equals("PHILHEALTH"))
            {
                // amount = salary switch
                // {
                //    > 0 and < 10000 => 400 / 2,
                //    > 10000 and < 79999.99M => (salary * 0.04M) / 2,
                //    _ => 3200 / 2
                // };

                var contribution = await _repoContribution.FirstOrDefaultAsync(new ContributionBySalariespec(adjustment.Name, payroll.PayrollDate, salary), cancellationToken);

                if (contribution is not null)
                {
                    if (contribution.IsFixed)
                    {
                        amount = contribution.EmployeeContribution / divisor;
                    }
                    else
                    {
                        amount = (salary * contribution.Percentage) / divisor / 2; // 2 (Employer-Employee) share
                    }

                    // Employer and Employee contributions are with the same amount
                    await SavePayrollDetail(employee, payroll, adjustment.Adapt<AdjustmentDto>(), amount, "COMPANY", cancellationToken);
                }
            }
            else if (adjustment.Name.Equals("SSS"))
            {
                var contribution = await _repoContribution.FirstOrDefaultAsync(new ContributionBySalariespec(adjustment.Name, payroll.PayrollDate, salary), cancellationToken);

                if (contribution is not null)
                {
                    amount = contribution.EmployeeContribution / divisor;

                    // Employer and Employee contributions are with the same amount
                    await SavePayrollDetail(employee, payroll, adjustment.Adapt<AdjustmentDto>(), contribution.EmployerContribution / divisor, "COMPANY", cancellationToken);
                }
            }
            else if (adjustment.Name.Equals("TAX"))
            {
                var contribution = await _repoContribution.FirstOrDefaultAsync(new ContributionBySalariespec(adjustment.Name, payroll.PayrollDate, salary), cancellationToken);

                if (contribution?.EmployeeContribution > 0)
                {
                    // amount = contribution.EmployeeContribution / divisor;
                }
            }
            else
            {
                var employeeAdjustment = await _repoEmployeeAdjustment.FirstOrDefaultAsync(new EmployeeAdjustmentByAdjustmentIdSpec(request.EmployeeId, adjustment.Id), cancellationToken);

                // Check On-Progress Employee Loan
                var employeeLoan = await _repoEmployeeLoan.FirstOrDefaultAsync(new LoanByEmployeeAdjustmentSpec(request.EmployeeId, adjustment.Id), cancellationToken);

                if (employeeAdjustment is null && employeeLoan is null)
                {
                    continue;
                }
                else
                {
                    if (employeeLoan is not null)
                    {
                        amount = employeeLoan.Ammortization;
                    }
                    else if (employeeAdjustment is not null)
                    {
                        amount = employeeAdjustment.Amount;
                    }
                }
            }

            if (amount > 0)
            {
                await SavePayrollDetail(employee, payroll, adjustment.Adapt<AdjustmentDto>(), amount, "EMPLOYEE", cancellationToken);

                amount = 0;
            }
        }

        // Save or Update Employee Payroll
        var employeePayrollDetails = await _repoEmployeePayrollDetail.ListAsync(new EmployeePayrollDetailByPayrollSpec(employee.Id, payroll.Id), cancellationToken);

        salary = employeePayrollDetails
            .Where(x => x.AdjustmentName.Equals("BASIC PAY"))
            .Select(x => x.Amount).Sum();

        decimal additionals = employeePayrollDetails
            .Where(x => x.AdjustmentType.Equals("ADDITIONAL") && !x.AdjustmentName.Equals("BASIC PAY"))
            .Select(x => x.Amount).Sum();

        decimal deductions = employeePayrollDetails
            .Where(x => x.AdjustmentType.Equals("DEDUCTION") && x.Contributor.Equals("EMPLOYEE"))
            .Select(x => x.Amount).Sum();

        var employeePayroll = await _repoEmployeePayroll.FirstOrDefaultAsync(new EmployeePayrollBySpec(employee.Id, payroll.Id), cancellationToken);
        if (employeePayroll is null)
        {
            var newEmployeePayroll = new EmployeePayroll(request.EmployeeId, employee.NameFull(), request.PayrollId, payroll.Name, salary, additionals, salary + additionals, deductions, (salary + additionals) - deductions, payroll.StartDate, payroll.EndDate, payroll.PayrollDate);
            await _repoEmployeePayroll.AddAsync(newEmployeePayroll, cancellationToken);
        }
        else
        {
            var updatedEmployeePayroll = employeePayroll.Update(employee.NameFull(), payroll.Name, salary, additionals, salary + additionals, deductions, (salary + additionals) - deductions, payroll.StartDate, payroll.EndDate, payroll.PayrollDate);
            await _repoEmployeePayroll.UpdateAsync(updatedEmployeePayroll, cancellationToken);
        }

        return true;
    }

    private async Task SavePayrollDetail(Employee employee, Payroll payroll, AdjustmentDto adjustment, decimal amount, string contributor, CancellationToken cancellationToken)
    {
        var employeePayrollDetail = await _repoEmployeePayrollDetail.FirstOrDefaultAsync(new EmployeePayrollDetailByPayrollAdjustmentSpec(employee.Id, payroll.Id, adjustment.Id, contributor), cancellationToken);
        if (employeePayrollDetail is null)
        {
            var newEmployeePayrollDetail = new EmployeePayrollDetail(employee.Id, employee.NameFull(), payroll.Id, payroll.Name, adjustment.Id, adjustment.AdjustmentType, adjustment.Name, amount, payroll.StartDate, payroll.EndDate, payroll.PayrollDate, contributor);
            await _repoEmployeePayrollDetail.AddAsync(newEmployeePayrollDetail, cancellationToken);
        }
        else
        {
            var updatedEmployeePayrollDetail = employeePayrollDetail.Update(employee.NameFull(), payroll.Name, adjustment.Name, amount, payroll.StartDate, payroll.EndDate, payroll.PayrollDate, contributor);
            await _repoEmployeePayrollDetail.UpdateAsync(updatedEmployeePayrollDetail, cancellationToken);
        }
    }
}