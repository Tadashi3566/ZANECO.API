using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanUpdateRequest : RequestWithImageExtension, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid AdjustmentId { get; set; }
    public DateTime DateReleased { get; set; }
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public int Months { get; set; } = default!;
    public decimal Ammortization { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class LoanUpdateRequestValidator : CustomValidator<LoanUpdateRequest>
{
    public LoanUpdateRequestValidator()
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

public class LoanUpdateRequestHandler : IRequestHandler<LoanUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<Loan> _repoLoan;
    private readonly IStringLocalizer<LoanUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public LoanUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<Loan> repoLoan, IStringLocalizer<LoanUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoAdjustment, _repoLoan, _localizer, _file) = (repoEmployee, repoAdjustment, repoLoan, localizer, file);

    public async Task<Guid> Handle(LoanUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        // Get Adjustment Information
        var adjustment = await _repoAdjustment.GetByIdAsync(request.AdjustmentId, cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.AdjustmentId} not found.");

        var loan = await _repoLoan.GetByIdAsync(request.Id, cancellationToken);
        _ = loan ?? throw new NotFoundException($"Loan {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = loan.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            loan = loan.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Loan>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedLoan = loan.Update(employee.NameFull(), request.AdjustmentId, adjustment.Name, request.Amount, request.DateReleased, request.PaymentSchedule, request.Months, request.Ammortization, request.StartDate, request.EndDate, request.Description, request.Notes, imagePath);

        await _repoLoan.UpdateAsync(updatedLoan, cancellationToken);

        return request.Id;
    }
}