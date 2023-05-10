using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentCreateRequest : IRequest<int>
{
    public DefaultIdType EmployeeId { get; set; }
    public string AppointmentType { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public DateTime StartDateTime { get; set; } = default!;
    public DateTime EndDateTime { get; set; } = default!;
    public string? Location { get; set; }
    public bool IsAllDay { get; set; } = default!;
    public bool IsReadonly { get; set; } = default!;
    public bool IsBlock { get; set; } = default!;
    public int? CalendarId { get; set; }
    public int? RecurrenceID { get; set; }
    public string? RecurrenceRule { get; set; }
    public string? RecurrenceException { get; set; }
    public string? CssClass { get; set; }

    public DefaultIdType? RecommendedBy { get; set; }
    public DateTime? RecommendedOn { get; set; }
    public DefaultIdType? ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public ImageUploadRequest? Image { get; set; }
}

public class CreateAppointmentRequestValidator : CustomValidator<AppointmentCreateRequest>
{
    public CreateAppointmentRequestValidator()
    {
        RuleFor(p => p.AppointmentType)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Subject)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class AppointmentCreateRequestHandler : IRequestHandler<AppointmentCreateRequest, int>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Appointment> _repository;
    private readonly IFileStorageService _file;

    public AppointmentCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Appointment> repository, IFileStorageService file) =>
        (_repoEmployee, _repository, _file) = (repoEmployee, repository, file);

    public async Task<int> Handle(AppointmentCreateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");

        if (!employee.IsActive) throw new Exception("Employee is no longer Active");

        string imagePath = await _file.UploadAsync<Appointment>(request.Image, FileType.Image, cancellationToken);

        if (request.IsAllDay)
        {
            request.StartDateTime = new DateTime(request.StartDateTime.Year, request.StartDateTime.Month, request.StartDateTime.Day);
            request.EndDateTime = new DateTime(request.EndDateTime.Year, request.EndDateTime.Month, request.EndDateTime.Day, 23, 59, 59);
        }

        var appointment = new Appointment(request.EmployeeId, employee!.NameFullInitial(), request.AppointmentType, request.Subject, request.StartDateTime, request.EndDateTime, request.Location, request.IsAllDay, request.IsReadonly, request.IsBlock, request.RecurrenceID, request.RecurrenceRule, request.RecurrenceException, request.CssClass, request.RecommendedBy, request.ApprovedBy, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(appointment, cancellationToken);

        return appointment.Id;
    }
}