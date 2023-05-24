using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollCreateRequest : IRequest<Guid>
{
    public string PayrollType { get; set; } = default!;
    public string EmploymentType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal TotalSalary { get; set; } = default!;
    public decimal TotalAdditional { get; set; } = default!;
    public decimal TotalGross { get; set; } = default!;
    public decimal TotalDeduction { get; set; } = default!;
    public decimal TotalNet { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public DateTime PayrollDate { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreatePayrollRequestValidator : CustomValidator<PayrollCreateRequest>
{
    public CreatePayrollRequestValidator(IReadRepository<Payroll> repoPayroll, IStringLocalizer<CreatePayrollRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (name, ct) => await repoPayroll.FirstOrDefaultAsync(new PayrollByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["payroll already exists"], name));

        //RuleFor(p => p.PayrollDate)
        //    .MustAsync(async (date, ct) => await repoPayroll.FirstOrDefaultAsync(new PayrollByPayrollDateSpec(date), ct) is null)
        //    .WithMessage((_, number) => string.Format(localizer["payroll already exists"], number));
    }
}

public class PayrollCreateRequestHandler : IRequestHandler<PayrollCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Payroll> _repository;
    private readonly IDateTimeFunctions _dateTimeFunctions;

    public PayrollCreateRequestHandler(IRepositoryWithEvents<Payroll> repository, IDateTimeFunctions dateTimeFunctions) =>
        (_repository, _dateTimeFunctions) = (repository, dateTimeFunctions);

    public async Task<Guid> Handle(PayrollCreateRequest request, CancellationToken cancellationToken)
    {
        int workingDays = _dateTimeFunctions.GetWorkingDays(request.StartDate, request.EndDate);

        // adjust working days when there are Holidays etc.

        var payroll = new Payroll(request.PayrollType, request.EmploymentType, request.Name, request.TotalSalary, request.TotalAdditional, request.TotalGross, request.TotalDeduction, request.TotalNet, request.StartDate, request.EndDate, workingDays, request.PayrollDate, request.Description, request.Notes);

        await _repository.AddAsync(payroll, cancellationToken);

        return payroll.Id;
    }
}