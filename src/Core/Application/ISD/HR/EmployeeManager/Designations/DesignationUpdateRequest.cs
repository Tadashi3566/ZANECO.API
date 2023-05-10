using ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;
using ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DesignationId { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid ScheduleId { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public string Area { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Division { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public string Position { get; set; } = default!;

    public string EmploymentType { get; set; } = default!;
    public int SalaryNumber { get; set; } = default!;
    public decimal SalaryAmount { get; set; } = default!;
    public string RateType { get; set; } = default!;
    public string PayType { get; set; } = "BI-MONTHLY";
    public decimal RatePerDay { get; set; } = default!;
    public decimal RatePerHour { get; set; } = default!;
    public string TaxType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class DesignationUpdateRequestValidator : CustomValidator<DesignationUpdateRequest>
{
    public DesignationUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DesignationUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.StartDate)
           .NotNull();

        RuleFor(p => p.Area)
           .NotEmpty();

        RuleFor(p => p.Department)
           .NotEmpty();

        RuleFor(p => p.Position)
           .NotEmpty();

        RuleFor(p => p.SalaryNumber)
           .NotEmpty();

        RuleFor(p => p.EmploymentType)
           .NotEmpty();

        RuleFor(p => p.ScheduleId)
           .NotEmpty();

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class DesignationUpdateRequestHandler : IRequestHandler<DesignationUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IReadRepository<Salary> _repoSalary;
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IRepositoryWithEvents<Designation> _repoDesignation;
    private readonly IFileStorageService _file;

    public DesignationUpdateRequestHandler(IRepositoryWithEvents<Employee> repoEmployee, IReadRepository<Salary> repoRank, IReadRepository<Schedule> repoSchedule, IRepositoryWithEvents<Designation> repoDesignation, IStringLocalizer<DesignationUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoSalary, _repoSchedule, _repoDesignation, _file) = (repoEmployee, repoRank, repoSchedule, repoDesignation, file);

    public async Task<Guid> Handle(DesignationUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("employee not found.");

        // Get Rank Information
        var salary = await _repoSalary.FirstOrDefaultAsync(new SalaryByNumberSpec(request.SalaryNumber), cancellationToken);
        _ = salary ?? throw new NotFoundException("salary not found.");

        // Get Schedule Information
        var schedule = await _repoSchedule.FirstOrDefaultAsync(new ScheduleByIdSpec(request.ScheduleId), cancellationToken);
        _ = schedule ?? throw new NotFoundException("schedule not found.");

        var designation = await _repoDesignation.GetByIdAsync(request.Id, cancellationToken);
        _ = designation ?? throw new NotFoundException("designation not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = designation.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            designation = designation.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedDesignation = designation.Update(employee.NameFull(), request.StartDate, request.EndDate, employee.RegularDate, request.Area, request.Department, request.Division, request.Section, request.Position, request.EmploymentType, request.SalaryNumber, salary.Name, salary!.Amount, salary.IncrementAmount, request.RatePerDay, salary.RateType, request.TaxType, request.PayType, request.ScheduleId, schedule.Name, request.Description, request.Notes, imagePath);

        await _repoDesignation.UpdateAsync(updatedDesignation, cancellationToken);

        return request.Id;
    }
}