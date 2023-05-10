using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
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

    public AppointmentActionRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Appointment> repoAppointment, IRepositoryWithEvents<Attendance> repoAttendance, IStringLocalizer<AppointmentActionRequestHandler> localizer) =>
        (_repoEmployee, _repoAppointment, _repoAttendance, _localizer) = (repoEmployee, repoAppointment, repoAttendance, localizer);

    public async Task<int> Handle(AppointmentActionRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repoAppointment.GetByIdAsync(request.Id, cancellationToken);
        _ = appointment ?? throw new NotFoundException(string.Format(_localizer["Appointment {0} not found."], request.Id));

        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException(string.Format(_localizer["Employee {0} not found."], request.Id));

        var attendances = await _repoAttendance.ListAsync(new AttendanceByDateRangeSpec(request.EmployeeId, appointment.StartDateTime, appointment.EndDateTime), cancellationToken);

        //Appointment appointmentAction = new();
        //Attendance attendanceAction = new();

        switch (request.Action)
        {
            case "SEND":
                _ = appointment.Send(request.EmployeeId);
                foreach (var attendanceDto in attendances)
                {
                    var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                    attendance?.Send(attendanceDto.Id);
                }

                break;

            case "CANCEL":
                _ = appointment.Cancel(request.EmployeeId);
                foreach (var attendanceDto in attendances)
                {
                    var attendance = await _repoAttendance.GetByIdAsync(attendanceDto.Id, cancellationToken);
                    attendance?.Cancel(attendanceDto.Id);
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