using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public string PayrollType { get; set; } = default!; // FULL MONTH, MONTH-MID & MONTH-END
    public string EmploymentType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal TotalSalary { get; set; } = default!;
    public decimal TotalAdditional { get; set; } = default!;
    public decimal TotalGross { get; set; } = default!;
    public decimal TotalDeduction { get; set; } = default!;
    public decimal TotalNet { get; set; } = default!;
    public int WorkingDays { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public DateTime PayrollDate { get; set; }
    public bool IsClosed { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class PayrollUpdateRequestValidator : CustomValidator<PayrollUpdateRequest>
{
    public PayrollUpdateRequestValidator(IReadRepository<Payroll> repoPayroll, IStringLocalizer<PayrollUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (payroll, name, ct) => await repoPayroll.FirstOrDefaultAsync(new PayrollByNameSpec(name), ct)
                        is not { } existingPayroll || existingPayroll.Id == payroll.Id)
            .WithMessage((_, name) => string.Format(localizer["payroll already exists"], name));

        //RuleFor(p => p.PayrollDate)
        //    .MustAsync(async (payroll, date, ct) => await repoPayroll.FirstOrDefaultAsync(new PayrollByPayrollDateSpec(date), ct)
        //                is not Payroll existingPayroll || existingPayroll.Id == payroll.Id)
        //    .WithMessage((_, number) => string.Format(localizer["payroll already exists"], number));
    }
}

public class PayrollUpdateRequestHandler : IRequestHandler<PayrollUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Payroll> _repository;
    private readonly IDateTimeFunctions _dateTimeFunctions;
    private readonly IStringLocalizer<PayrollUpdateRequestHandler> _localizer;

    public PayrollUpdateRequestHandler(IRepositoryWithEvents<Payroll> repository, IDateTimeFunctions dateTimeFunctions, IStringLocalizer<PayrollUpdateRequestHandler> localizer) =>
        (_repository, _dateTimeFunctions, _localizer) = (repository, dateTimeFunctions, localizer);

    public async Task<Guid> Handle(PayrollUpdateRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = payroll ?? throw new NotFoundException(string.Format(_localizer["payroll not found."], request.Id));

        int workingDays = _dateTimeFunctions.GetWorkingDays(request.StartDate, request.EndDate);

        // adjust working days when there are Holidays etc.

        var updatedPayroll = payroll.Update(request.PayrollType, request.EmploymentType, request.Name, request.TotalSalary, request.TotalAdditional, request.TotalGross, request.TotalDeduction, request.TotalNet, request.StartDate, request.EndDate, workingDays, request.PayrollDate, request.IsClosed, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedPayroll, cancellationToken);

        return request.Id;
    }
}