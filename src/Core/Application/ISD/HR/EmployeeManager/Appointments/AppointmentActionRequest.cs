using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
using ZANECO.API.Application.SMS;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentActionRequest : IRequest<int>
{
    public int Id { get; set; }
    public string Action { get; set; } = default!;
    public DefaultIdType EmployeeId { get; set; }
}

public class AppointmentActionRequestValidator : CustomValidator<AppointmentActionRequest>
{
    public AppointmentActionRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();
    }
}

public class AppointmentActionRequestHandler : IRequestHandler<AppointmentActionRequest, int>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Appointment> _repoAppointment;
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IStringLocalizer<AppointmentActionRequestHandler> _localizer;
    private readonly ICurrentUser _currentUser;
    private readonly ISmsService _smsService;
    private readonly IJobService _jobService;

    public AppointmentActionRequestHandler(
        IReadRepository<Employee> repoEmployee,
        IRepositoryWithEvents<Appointment> repoAppointment,
        IRepositoryWithEvents<Attendance> repoAttendance,
        IStringLocalizer<AppointmentActionRequestHandler> localizer,
        ICurrentUser currentUser,
        ISmsService smsService,
        IJobService jobService
        ) =>
        (_repoEmployee, _repoAppointment, _repoAttendance, _localizer, _currentUser, _smsService, _jobService)
        = (repoEmployee, repoAppointment, repoAttendance, localizer, currentUser, smsService, jobService);

    public async Task<int> Handle(AppointmentActionRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repoAppointment.GetByIdAsync(request.Id, cancellationToken);
        _ = appointment ?? throw new NotFoundException(string.Format(_localizer["Appointment {0} not found."], request.Id));

        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException(string.Format(_localizer["Employee {0} not found."], request.Id));

        var attendances = await _repoAttendance.ListAsync(new AttendanceByDateRangeSpec(request.EmployeeId, appointment.StartDateTime, appointment.EndDateTime), cancellationToken);

        string? phoneNumber = _currentUser.GetPhoneNumber();

        switch (request.Action)
        {
            case "SEND":
                _ = appointment.Send(request.EmployeeId);
                foreach (var attendanceDto in attendances)
                {
                    var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                    attendance?.Send(attendanceDto.Id);

                    if (phoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(phoneNumber, $"Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully sent to your Recommender.", true, "sms.automatic"));
                }

                break;

            case "CANCEL":
                _ = appointment.Cancel(request.EmployeeId);
                foreach (var attendanceDto in attendances)
                {
                    var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                    attendance?.Cancel(attendanceDto.Id);

                    //if (phoneNumber is not null)
                    //    _jobService.Enqueue(() => _smsService.SmsSend(phoneNumber, $"Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been Cancelled.", true, "sms.automatic"));
                }

                break;

            case "RECOMMEND":
                if (employee is not null && appointment.RecommendedBy.Equals(request.EmployeeId))
                {
                    _ = appointment.Recommend(employee.Id, employee.FullName());

                    foreach (var attendanceDto in attendances)
                    {
                        var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                        attendance?.Recommend(attendanceDto.Id, employee.FullName());

                        if (phoneNumber is not null)
                            _jobService.Enqueue(() => _smsService.SmsSend(phoneNumber, $"Your Appointment Id: {appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully sent to your Approver.", true, "sms.automatic"));
                    }
                }
                else
                {
                    throw new Exception("Please login as the Recommender or inform the personnel to update the Recommender.");
                }

                break;

            case "APPROVE":
                if (employee is not null && appointment.ApprovedBy.Equals(request.EmployeeId))
                {
                    _ = appointment.Approve(employee.Id, employee.FullName());

                    foreach (var attendanceDto in attendances)
                    {
                        var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                        attendance?.Approve(attendanceDto.Id, employee.FullName(), appointment.AppointmentType, appointment.Subject);

                        if (phoneNumber is not null)
                            _jobService.Enqueue(() => _smsService.SmsSend(phoneNumber, $"Your Appointment Id: {appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully Approved.", true, "sms.automatic"));
                    }
                }
                else
                {
                    throw new Exception("Please login as the Approver or inform the personnel to update the Approver.");
                }

                break;

            case "DISAPPROVE":
                if (employee is not null && appointment.ApprovedBy.Equals(request.EmployeeId))
                {
                    _ = appointment.Disapprove(request.EmployeeId, employee.FullName());

                    foreach (var attendanceDto in attendances)
                    {
                        var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                        attendance?.Disapprove(attendanceDto.Id, employee.FullName());

                        if (phoneNumber is not null)
                            _jobService.Enqueue(() => _smsService.SmsSend(phoneNumber, $"Your Appointment {appointment.AppointmentType} on {appointment.StartDateTime:M} has been Disapproved.", true, "sms.automatic"));
                    }
                }
                else
                {
                    throw new Exception("Please login as the Approver or inform the personnel to update the Approver.");
                }

                break;
        }

        await _repoAppointment.UpdateAsync(appointment, cancellationToken);

        return request.Id;
    }
}