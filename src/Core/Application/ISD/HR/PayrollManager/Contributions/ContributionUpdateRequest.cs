using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
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
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class ContributionUpdateRequestValidator : CustomValidator<ContributionUpdateRequest>
{
    public ContributionUpdateRequestValidator()
    {
        RuleFor(p => p.ContributionType)
            .NotEmpty();

        RuleFor(p => p.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-10));

        RuleFor(p => p.EndDate)
            .GreaterThanOrEqualTo(DateTime.Today.AddYears(-10));
    }
}

public class ContributionUpdateRequestHandler : IRequestHandler<ContributionUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contribution> _repository;
    private readonly IStringLocalizer<ContributionUpdateRequestHandler> _localizer;

    public ContributionUpdateRequestHandler(IRepositoryWithEvents<Contribution> repository, IStringLocalizer<ContributionUpdateRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ContributionUpdateRequest request, CancellationToken cancellationToken)
    {
        var contribution = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = contribution ?? throw new NotFoundException(string.Format(_localizer["Contribution not found."], request.Id));

        var updatedContribution = contribution.Update(request.ContributionType, request.StartDate, request.EndDate, request.RangeStart, request.RangeEnd, request.EmployerContribution, request.EmployeeContribution, request.TotalContribution, request.Percentage, request.IsFixed, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedContribution, cancellationToken);

        return request.Id;
    }
}