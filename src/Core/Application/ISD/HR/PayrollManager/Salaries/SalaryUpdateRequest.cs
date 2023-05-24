using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string RateType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public int IncrementYears { get; set; } = default!;
    public decimal IncrementAmount { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class SalaryUpdateRequestValidator : CustomValidator<SalaryUpdateRequest>
{
    public SalaryUpdateRequestValidator(IReadRepository<Salary> repository, IStringLocalizer<SalaryUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThanOrEqualTo(0)
            .MustAsync(async (salary, number, ct) =>
                    await repository.FirstOrDefaultAsync(new SalaryByNumberSpec(number), ct)
                        is not Salary existingSalary || existingSalary.Id == salary.Id)
                .WithMessage((_, number) => string.Format(localizer["salary already exists"], number));

        RuleFor(p => p.Amount)
            .GreaterThan(0);

        RuleFor(p => p.IncrementYears)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.IncrementAmount)
            .GreaterThanOrEqualTo(0);
    }
}

public class SalaryUpdateRequestHandler : IRequestHandler<SalaryUpdateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Salary> _repository;
    private readonly IStringLocalizer<SalaryUpdateRequestHandler> _localizer;

    public SalaryUpdateRequestHandler(IRepositoryWithEvents<Salary> repository, IStringLocalizer<SalaryUpdateRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(SalaryUpdateRequest request, CancellationToken cancellationToken)
    {
        var salary = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = salary ?? throw new NotFoundException(string.Format(_localizer["salary not found."], request.Id));

        var updatedSalary = salary.Update(request.StartDate, request.EndDate, request.RateType, request.Number, request.Name, request.Amount, request.IncrementYears, request.IncrementAmount, request.IsActive, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedSalary, cancellationToken);

        return request.Id;
    }
}