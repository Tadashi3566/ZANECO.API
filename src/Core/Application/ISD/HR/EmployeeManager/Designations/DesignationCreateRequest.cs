using ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;
using ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationCreateRequest : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid DesignationId { get; set; } = default!;
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
    public string PayType { get; set; } = default!;
    public decimal RatePerDay { get; set; } = default!;
    public decimal RatePerHour { get; set; } = default!;
    public string TaxType { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Notes { get; set; }

    public ImageUploadRequest? Image { get; set; }
}

public class DesignationCreateRequestValidator : CustomValidator<DesignationCreateRequest>
{
    public DesignationCreateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DesignationCreateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.StartDate)
            .NotNull();

        RuleFor(p => p.EndDate)
            .NotNull();

        RuleFor(p => p.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End Date should be greater than Start Date");

        RuleFor(p => p.Area)
           .NotEmpty();

        RuleFor(p => p.Department)
           .NotEmpty();

        RuleFor(p => p.Position)
           .NotEmpty();

        RuleFor(p => p.PayType)
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

public class DesignationCreateRequestHandler : IRequestHandler<DesignationCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Designation> _repoDesignation;
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IReadRepository<Salary> _repoSalary;
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IDapperRepository _dapper;
    private readonly IFileStorageService _file;

    public DesignationCreateRequestHandler(IRepositoryWithEvents<Designation> repoDesignation, IRepositoryWithEvents<Employee> repoEmployee, IReadRepository<Salary> repoRank, IReadRepository<Schedule> repoSchedule, IDapperRepository dapper, IFileStorageService file) =>
        (_repoDesignation, _repoEmployee, _repoSalary, _repoSchedule, _dapper, _file) = (repoDesignation, repoEmployee, repoRank, repoSchedule, dapper, file);

    public async Task<Guid> Handle(DesignationCreateRequest request, CancellationToken cancellationToken)
    {
        if (request.StartDate >= request.EndDate)
        {
            throw new ArgumentException("End Date should be greater then Start Date");
        }

        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");

        // Get Rank Information
        var salary = await _repoSalary.FirstOrDefaultAsync(new SalaryByNumberSpec(request.SalaryNumber), cancellationToken);
        _ = salary ?? throw new NotFoundException("Salary not found.");

        // Get Schedule Information
        var schedule = await _repoSchedule.FirstOrDefaultAsync(new ScheduleByIdSpec(request.ScheduleId), cancellationToken);
        _ = schedule ?? throw new NotFoundException("Schedule not found.");

        // Update last designation End Date
        var lastDesignation = await _repoDesignation.FirstOrDefaultAsync(new DesignationLastSpec(request.EmployeeId), cancellationToken);
        if (lastDesignation is not null)
        {
            var updatedLastDesignation = lastDesignation.Deactivate(request.StartDate.AddDays(-1));
            await _repoDesignation.UpdateAsync(updatedLastDesignation, cancellationToken);
        }

        // Set all Employee Designations as Active=false
        // await _dapper.ExecuteScalarAsync<Designation>($"UPDATE datazaneco.designations SET IsActive = 0 WHERE EmployeeId LIKE '{employee.Id}'", cancellationToken: cancellationToken);

        string imagePath = await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken);

        var designation = new Designation(request.EmployeeId, employee.Number, employee.NameFull(), request.StartDate, request.EndDate, employee.RegularDate, request.Area, request.Department, request.Division, request.Section, request.Position, request.EmploymentType, salary.Number, salary.Name, salary.Amount, salary.IncrementAmount, request.RatePerDay, salary.RateType, request.TaxType, request.PayType, request.ScheduleId, schedule.Name, request.Description, request.Notes, imagePath);
        await _repoDesignation.AddAsync(designation, cancellationToken);

        // Update Employee Designation
        var employeeDesignation = employee.Designation(request.DesignationId, designation.StartDate, employee.RegularDate, designation.Area, designation.Department, designation.Division, designation.Section, designation.Position, designation.EmploymentType, salary.Number, salary.Name, salary.Amount, salary.IncrementAmount, designation.RateType, designation.HoursPerDay, designation.TaxType, designation.PayType, schedule.Id, schedule.Name, request.EndDate > DateTime.Today);
        await _repoEmployee.UpdateAsync(employeeDesignation, cancellationToken);

        return designation.Id;
    }
}