using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
using ZANECO.API.Application.SMS;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentActionRequest : IRequest<int>
{
    public int Id { get; set; }
    public DefaultIdType UserEmployeeId { get; set; } = default!;
    public string Action { get; set; } = default!;
}

public class AppointmentActionRequestValidator : CustomValidator<AppointmentActionRequest>
{
    public AppointmentActionRequestValidator()
    {
        RuleFor(p => p.UserEmployeeId)
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
        IJobService jobService)
        => (_repoEmployee, _repoAppointment, _repoAttendance, _localizer, _currentUser, _smsService, _jobService)
        = (repoEmployee, repoAppointment, repoAttendance, localizer, currentUser, smsService, jobService);

    public async Task<int> Handle(AppointmentActionRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repoAppointment.GetByIdAsync(request.Id, cancellationToken);
        _ = appointment ?? throw new NotFoundException(string.Format(_localizer["Appointment {0} not found."], request.Id));

        var appointmentEmployee = await _repoEmployee.GetByIdAsync(appointment.EmployeeId, cancellationToken);
        _ = appointmentEmployee ?? throw new NotFoundException(string.Format(_localizer["Employee {0} not found."], request.Id));

        var recommender = await _repoEmployee.GetByIdAsync(appointment.RecommendedBy, cancellationToken);
        _ = recommender ?? throw new NotFoundException(string.Format(_localizer["Employee {0} not found."], request.Id));

        var approver = await _repoEmployee.GetByIdAsync(appointment.ApprovedBy, cancellationToken);
        _ = approver ?? throw new NotFoundException(string.Format(_localizer["Employee {0} not found."], request.Id));

        var attendances = await _repoAttendance.ListAsync(new AttendanceByDateRangeSpec(appointment.EmployeeId, appointment.StartDateTime, appointment.EndDateTime), cancellationToken);

        string? userPhoneNumber = _currentUser.GetPhoneNumber();

        switch (request.Action)
        {
            case "SEND":
                _ = appointment.Send(appointment.EmployeeId);

                if (appointmentEmployee.PhoneNumber is not null)
                    _jobService.Enqueue(() => _smsService.SmsSend(appointmentEmployee.PhoneNumber, $"Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully sent as For Recommendation.", false, true, "sms.automatic")); //You may also update your Appointment Information or contact your Supervisor if there are some details were not provided on your Appointment.

                if (recommender.PhoneNumber is not null)
                    _jobService.Enqueue(() => _smsService.SmsSend(recommender.PhoneNumber, $"You have received an Appointment Id:{appointment.Id} of Employee:{appointment.EmployeeName} on {appointment.StartDateTime:M} for your Recommendation.{Environment.NewLine}{appointment.AppointmentType}-{appointment.Subject}.{Environment.NewLine}http://app.zaneco.ph/employee/appointments/{appointment.Id}", false, true, "sms.automatic")); //You may open the ZANECO HRIS Web App and proceed to the Appointments for details or you may Cancel if Appointment Justifications seems invalid or needs update.

                break;

            case "CANCEL":
                _ = appointment.Cancel(request.UserEmployeeId);

                if (userPhoneNumber is not null)
                    _jobService.Enqueue(() => _smsService.SmsSend(userPhoneNumber, $"You have successfully Cancelled the Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M}.", false, true, "sms.automatic")); //You may contact the employee if you need more details or clarifications from the Appointment Information.

                if (appointmentEmployee.PhoneNumber is not null)
                    _jobService.Enqueue(() => _smsService.SmsSend(appointmentEmployee.PhoneNumber, $"Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been Cancelled.", false, true, "sms.automatic"));

                break;

            case "RECOMMEND":
                if (appointmentEmployee is not null && appointment.RecommendedBy.Equals(request.UserEmployeeId))
                {
                    _ = appointment.Recommend(recommender.Id, recommender.FullName());

                    if (appointmentEmployee.PhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(appointmentEmployee.PhoneNumber, $"Your Appointment Id: {appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully sent to your Approver.", false, true, "sms.automatic")); //You may also contact your Manager if there are more details that were not provided on your Appointment Information.

                    if (userPhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(userPhoneNumber, $"You have successfully set the Appointment Id:{appointment.Id} {appointment.AppointmentType} of Employee: {appointment.EmployeeName} on {appointment.StartDateTime:M} as Recommended to the Approver.", false, true, "sms.automatic")); //You may contact the Employee or the Approver if there are some details that were not provided on the Appointment Information.

                    if (approver.PhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(approver.PhoneNumber, $"You have received an Appointment Id:{appointment.Id} of Employee:{appointment.EmployeeName} on {appointment.StartDateTime:M} for your Approval.{Environment.NewLine}{appointment.AppointmentType}-{appointment.Subject}.{Environment.NewLine}http://app.zaneco.ph/employee/appointments/{appointment.Id}", false, true, "sms.automatic")); //You may open the ZANECO HRIS Web App for more details and/or you may Disapprove if the Appointment Justifications seems invalid.
                }
                else
                {
                    throw new Exception("Please login as the Recommender or inform the personnel to update the Recommender.");
                }

                break;

            case "APPROVE":
                if (approver is not null && appointment.ApprovedBy.Equals(request.UserEmployeeId))
                {
                    _ = appointment.Approve(approver.Id, approver.FullName());

                    foreach (var attendanceDto in attendances)
                    {
                        var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                        attendance?.Approve(attendanceDto.Id, appointmentEmployee.FullName(), appointment.AppointmentType, appointment.Subject);
                    }

                    if (appointmentEmployee.PhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(appointmentEmployee.PhoneNumber, $"Congratualations! Your Appointment Id:{appointment.Id} {appointment.AppointmentType} on {appointment.StartDateTime:M} has been successfully Approved.", false, true, "sms.automatic"));

                    if (userPhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(userPhoneNumber, $"You have successfully Approved an Appointment Id:{appointment.Id} {appointment.AppointmentType} of Employee: {appointment.EmployeeName} on {appointment.StartDateTime:M}.", false, true, "sms.automatic")); //You may contact the Employee to address applicable conditions if there's any.
                }
                else
                {
                    throw new Exception("Please login as the Approver or inform the personnel to update the Approver.");
                }

                break;

            case "DISAPPROVE":
                if (approver is not null && appointment.ApprovedBy.Equals(request.UserEmployeeId))
                {
                    _ = appointment.Disapprove(request.UserEmployeeId, appointmentEmployee.FullName());

                    if (appointmentEmployee.PhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(appointmentEmployee.PhoneNumber, $"Your Appointment {appointment.AppointmentType} on {appointment.StartDateTime:M} has been Disapproved. You may contact your Approver why your Appointment Justifications are invalid.", false, true, "sms.automatic"));

                    if (userPhoneNumber is not null)
                        _jobService.Enqueue(() => _smsService.SmsSend(userPhoneNumber, $"You have Dispproved an Appointment Id:{appointment.Id} {appointment.AppointmentType} of Employee: {appointmentEmployee.FullInitialName()} on {appointment.StartDateTime:M}. You may contact the Employee why such Appointment Justifications are invalid.", false, true, "sms.automatic"));
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