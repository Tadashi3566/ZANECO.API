using ZANECO.API.Application.SMS;
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
    public int Hours { get; set; } = default!;
    public bool IsAllDay { get; set; } = default!;

    //public bool IsReadonly { get; set; } = default!;
    //public bool IsBlock { get; set; } = default!;
    //public int? CalendarId { get; set; }
    //public int? RecurrenceID { get; set; }
    //public string? RecurrenceRule { get; set; }
    //public string? RecurrenceException { get; set; }
    //public string? CssClass { get; set; }

    public DefaultIdType? RecommendedBy { get; set; }
    public DateTime? RecommendedOn { get; set; }
    public DefaultIdType? ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }
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
    private readonly ICurrentUser _currentUser;
    private readonly ISmsService _smsService;
    private readonly IJobService _jobService;
    private readonly IFileStorageService _file;

    public AppointmentCreateRequestHandler(
        IReadRepository<Employee> repoEmployee,
        IRepositoryWithEvents<Appointment> repository,
        ICurrentUser currentUser,
        ISmsService smsService,
        IJobService jobService,
        IFileStorageService file) =>
        (_repoEmployee, _repository, _currentUser, _smsService, _jobService, _file) = (repoEmployee, repository, currentUser, smsService, jobService, file);

    public async Task<int> Handle(AppointmentCreateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        //Check if Approvals has been filled up
        if (request.ApprovedBy.Equals(Guid.Empty) || request.RecommendedBy.Equals(Guid.Empty))
        {
            //Get recent Recommender and Approver
            var recentAppointment = await _repository.FirstOrDefaultAsync(new AppointmentRecentSpec(request.EmployeeId), cancellationToken);

            if (recentAppointment is not null)
            {
                request.RecommendedBy = recentAppointment.RecommendedBy;
                request.ApprovedBy = recentAppointment.ApprovedBy;
            }
            else
            {
                throw new NotFoundException("Please provide Recommender and Approver to continue.");
            }
        }

        string imagePath = await _file.UploadAsync<Appointment>(request.Image, FileType.Image, cancellationToken);

        if (request.IsAllDay)
        {
            request.StartDateTime = new DateTime(request.StartDateTime.Year, request.StartDateTime.Month, request.StartDateTime.Day);
            request.EndDateTime = new DateTime(request.EndDateTime.Year, request.EndDateTime.Month, request.EndDateTime.Day, 23, 59, 59);
        }

        var appointment = new Appointment(request.EmployeeId, employee!.NameFullInitial(), request.AppointmentType, request.Subject, request.StartDateTime, request.EndDateTime, request.Location, request.Hours, request.IsAllDay, request.RecommendedBy, request.ApprovedBy, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(appointment, cancellationToken);

        if (employee.PhoneNumber is not null)
            _jobService.Enqueue(() => _smsService.SmsSend(employee.PhoneNumber, $"Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully sent as For Recommendation.", false, true, "sms.automatic")); //You may also update your Appointment Information or contact your Supervisor if there are some details were not provided on your Appointment.

        string? userPhoneNumber = _currentUser.GetPhoneNumber();

        if (userPhoneNumber is not null)
            _jobService.Enqueue(() => _smsService.SmsSend(userPhoneNumber, $"You have received an Appointment Id:{appointment.Id} of Employee:{appointment.EmployeeName} on {appointment.StartDateTime:M} for your Recommendation.{Environment.NewLine}{appointment.AppointmentType}-{appointment.Subject}.{Environment.NewLine}http://app.zaneco.ph/employee/appointment/{appointment.Id}", false, true, "sms.automatic")); //You may open the ZANECO HRIS Web App and proceed to the Appointments for details or you may Cancel if Appointment Justifications seems invalid or needs update.

        return appointment.Id;
    }
}