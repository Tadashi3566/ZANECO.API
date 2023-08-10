using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionCreateRequest : BaseRequest, IRequest<Guid>
{
    public string ContributionType { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public decimal RangeStart { get; set; } = default!;
    public decimal RangeEnd { get; set; } = default!;
    public decimal EmployerContribution { get; set; } = default!;
    public decimal EmployeeContribution { get; set; } = default!;
    public decimal TotalContribution { get; set; } = default!;
    public decimal Percentage { get; set; } = default!;
    public bool IsFixed { get; set; } = default!;
}

public class CreateContributionRequestValidator : CustomValidator<ContributionCreateRequest>
{
    public CreateContributionRequestValidator()
    {
        RuleFor(p => p.ContributionType)
            .NotEmpty();

        RuleFor(p => p.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-10));

        RuleFor(p => p.EndDate)
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-10));
    }
}

public class ContributionCreateRequestHandler : IRequestHandler<ContributionCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contribution> _repository;

    public ContributionCreateRequestHandler(IRepositoryWithEvents<Contribution> repository) => _repository = repository;

    public async Task<Guid> Handle(ContributionCreateRequest request, CancellationToken cancellationToken)
    {
        var contribution = new Contribution(request.ContributionType, request.StartDate, request.EndDate, request.RangeStart, request.RangeEnd, request.EmployerContribution, request.EmployeeContribution, request.TotalContribution, request.Percentage, request.IsFixed, request.Description, request.Notes);

        await _repository.AddAsync(contribution, cancellationToken);

        return contribution.Id;
    }
}