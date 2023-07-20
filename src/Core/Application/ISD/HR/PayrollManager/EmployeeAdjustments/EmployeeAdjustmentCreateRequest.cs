using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentCreateRequest : IRequest<Guid>
{
    public Guid EmployeeId { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public Guid AdjustmentId { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime? EndDate { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class EmployeeAdjustmentCreateRequestValidator : CustomValidator<EmployeeAdjustmentCreateRequest>
{
    public EmployeeAdjustmentCreateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<EmployeeAdjustmentCreateRequestValidator> localizer)
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

public class EmployeeAdjustmentCreateRequestHandler : IRequestHandler<EmployeeAdjustmentCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Adjustment> _repoAdjustment;
    private readonly IRepositoryWithEvents<EmployeeAdjustment> _repoEmployeeAdjustment;
    private readonly IStringLocalizer<EmployeeAdjustmentCreateRequest> _localizer;
    private readonly IFileStorageService _file;

    public EmployeeAdjustmentCreateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Adjustment> repoAdjustment, IRepositoryWithEvents<EmployeeAdjustment> repoEmployeeAdjustment, IStringLocalizer<EmployeeAdjustmentCreateRequest> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoAdjustment, _repoEmployeeAdjustment, _localizer, _file) = (repoEmployee, repoAdjustment, repoEmployeeAdjustment, localizer, file);

    public async Task<Guid> Handle(EmployeeAdjustmentCreateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");
        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        //Check if already exist
        var existingEmployeeAdjustment = await _repoEmployeeAdjustment.FirstOrDefaultAsync(new EmployeeAdjustmentByAdjustmentIdSpec(request.EmployeeId, request.AdjustmentId), cancellationToken);
        if (existingEmployeeAdjustment is not null)
            throw new NotFoundException($"Employee Adjustment {request.AdjustmentId} already exist.");

        // Get Adjustment Information
        var adjustment = await _repoAdjustment.GetByIdAsync(request.AdjustmentId, cancellationToken);
        _ = adjustment ?? throw new NotFoundException($"Adjustment {request.AdjustmentId} not found.");

        decimal adjustmentAmount = request.Amount.Equals(0) ? adjustment.Amount : request.Amount;

        string imagePath = await _file.UploadAsync<EmployeeAdjustment>(request.Image, FileType.Image, cancellationToken);

        var employeeAdjustment = new EmployeeAdjustment(employee.Id, employee.NameFull(), adjustment.Id, adjustment.AdjustmentType, request.PaymentSchedule, adjustment.Name, adjustmentAmount, request.StartDate, request.EndDate, request.Description, request.Notes, imagePath);

        await _repoEmployeeAdjustment.AddAsync(employeeAdjustment, cancellationToken);

        return employeeAdjustment.Id;
    }
}