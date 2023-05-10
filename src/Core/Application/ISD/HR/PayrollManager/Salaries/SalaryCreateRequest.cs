using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryCreateRequest : IRequest<DefaultIdType>
{
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string RateType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public int IncrementYears { get; set; } = default!;
    public decimal IncrementAmount { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class CreateSalaryRequestValidator : CustomValidator<SalaryCreateRequest>
{
    public CreateSalaryRequestValidator(IReadRepository<Salary> repoSalary, IStringLocalizer<CreateSalaryRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThanOrEqualTo(0)
            .MustAsync(async (number, ct) => await repoSalary.FirstOrDefaultAsync(new SalaryByNumberSpec(number), ct) is null)
            .WithMessage((_, number) => string.Format(localizer["salary already exists"], number));

        // RuleFor(p => p.Name)
        //    .NotEmpty()
        //    .MaximumLength(16)
        //    .MustAsync(async (name, ct) => await repoSalary.FirstOrDefaultAsync(new SalaryByNameSpec(name), ct) is null)
        //    .WithMessage((_, name) => string.Format(localizer["salary already exists"], name));

        RuleFor(p => p.Amount)
            .GreaterThan(0);

        RuleFor(p => p.IncrementYears)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.IncrementAmount)
            .GreaterThanOrEqualTo(0);
    }
}

public class SalaryCreateRequestHandler : IRequestHandler<SalaryCreateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Salary> _repository;

    public SalaryCreateRequestHandler(IRepositoryWithEvents<Salary> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(SalaryCreateRequest request, CancellationToken cancellationToken)
    {
        var salary = new Salary(request.StartDate, request.EndDate, request.RateType, request.Number, request.Name, request.Amount, request.IncrementYears, request.IncrementAmount, request.IsActive, request.Description, request.Notes);

        await _repository.AddAsync(salary, cancellationToken);

        return salary.Id;
    }
}