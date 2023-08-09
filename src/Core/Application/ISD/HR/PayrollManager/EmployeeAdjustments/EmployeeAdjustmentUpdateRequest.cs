using ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentUpdateRequest : RequestWithImageExtension<EmployeeAdjustmentUpdateRequest>, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string PaymentSchedule { get; set; } = default!;
    public Guid AdjustmentId { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class EmployeeAdjustmentUpdateRequestValidator : CustomValidator<EmployeeAdjustmentUpdateRequest>
{
    public EmployeeAdjustmentUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<EmployeeAdjustmentUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.AdjustmentId)
            .NotEmpty();

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class EmployeeAdjustmentUpdateRequestHandler : IRequestHandler<EmployeeAdjustmentUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<EmployeeAdjustment> _repoEmployeeAdjustment;
    private readonly IStringLocalizer<EmployeeAdjustmentUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public EmployeeAdjustmentUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<EmployeeAdjustment> repoEmployeeAdjustment, IStringLocalizer<EmployeeAdjustmentUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoAdjustment, _repoEmployeeAdjustment, _localizer, _file) = (repoEmployee, repoAdjustment, repoEmployeeAdjustment, localizer, file);

    public async Task<Guid> Handle(EmployeeAdjustmentUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        // Get Adjustment Information
        var adjustment = await _repoAdjustment.FirstOrDefaultAsync(new AdjustmentByIdSpec(request.AdjustmentId), cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.AdjustmentId} not found.");

        var employeeAdjustment = await _repoEmployeeAdjustment.GetByIdAsync(request.Id, cancellationToken);
        _ = employeeAdjustment ?? throw new NotFoundException($"Employee Adjustment {request.Id} not found.");

        decimal adjustmentAmount = request.Amount.Equals(0) ? adjustment.Amount : request.Amount;

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = employeeAdjustment.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            employeeAdjustment = employeeAdjustment.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<EmployeeAdjustment>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedEmployeeAdjustment = employeeAdjustment.Update(employee.NameFull(), adjustment.Id, adjustment.AdjustmentType, request.PaymentSchedule, adjustment.Name, adjustmentAmount, request.StartDate, request.EndDate, request.Description, request.Notes, imagePath);

        await _repoEmployeeAdjustment.UpdateAsync(updatedEmployeeAdjustment, cancellationToken);

        return request.Id;
    }
}