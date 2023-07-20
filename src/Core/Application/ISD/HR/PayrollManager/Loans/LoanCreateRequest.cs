using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanCreateRequest : IRequest<Guid>
{
    public Guid EmployeeId { get; set; } = default!;
    public Guid AdjustmentId { get; set; } = default!;
    public DateTime DateReleased { get; set; }
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public int Months { get; set; } = default!;
    public decimal Ammortization { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class LoanCreateRequestValidator : CustomValidator<LoanCreateRequest>
{
    public LoanCreateRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();

        RuleFor(p => p.AdjustmentId)
            .NotEmpty();

        RuleFor(p => p.Amount)
            .GreaterThan(0);

        RuleFor(p => p.PaymentSchedule)
            .NotEmpty();

        RuleFor(p => p.Months)
            .GreaterThan(0);

        RuleFor(p => p.Ammortization)
            .GreaterThan(0);

        RuleFor(p => p.Image)
           .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class LoanCreateRequestHandler : IRequestHandler<LoanCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<Loan> _repoLoan;
    private readonly IFileStorageService _file;

    public LoanCreateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<Loan> repoLoan, IFileStorageService file) =>
        (_repoEmployee, _repoAdjustment, _repoLoan, _file) = (repoEmployee, repoAdjustment, repoLoan, file);

    public async Task<Guid> Handle(LoanCreateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");
        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        string imagePath = await _file.UploadAsync<Loan>(request.Image, FileType.Image, cancellationToken);

        // Get Adjustment Information
        var adjustment = await _repoAdjustment.GetByIdAsync(request.AdjustmentId, cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.AdjustmentId} not found.");

        var loan = new Loan(request.EmployeeId, employee.NameFull(), request.AdjustmentId, adjustment.Name, request.Amount, request.DateReleased, request.PaymentSchedule, request.Months, request.Ammortization, request.StartDate, request.EndDate, request.Description, request.Notes, imagePath);

        await _repoLoan.AddAsync(loan, cancellationToken);

        return loan.Id;
    }
}