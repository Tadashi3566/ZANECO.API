using ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;
using ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class CreateAttendanceRequestValidator : CustomValidator<AttendanceCreateRequest>
{
    public CreateAttendanceRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();
    }
}

public class AttendanceCreateRequestHandler : IRequestHandler<AttendanceCreateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IReadRepository<Calendar> _repoCalendar;
    private readonly IReadRepository<ScheduleDetail> _repoScheduleDetail;
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;

    public AttendanceCreateRequestHandler(
        IReadRepository<Employee> repoEmployee,
        IReadRepository<Schedule> repoSchedule,
        IReadRepository<Calendar> repoCalendar,
        IReadRepository<ScheduleDetail> repoScheduleDetail,
        IRepositoryWithEvents<Attendance> repoAttendance)
        => (_repoEmployee, _repoSchedule, _repoCalendar, _repoScheduleDetail, _repoAttendance) =
                                    (repoEmployee, repoSchedule, repoCalendar, repoScheduleDetail, repoAttendance);

    public async Task<DefaultIdType> Handle(AttendanceCreateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        var schedule = await _repoSchedule.GetByIdAsync(employee!.ScheduleId, cancellationToken);

        if (schedule is null)
        {
            return DefaultIdType.Empty;
        }

        List<DateTime> dateList = new();

        for (var date = request.StartDate!; date <= request.EndDate; date = date.AddDays(1))
        {
            dateList.Add(date);
        }

        foreach (var date in dateList)
        {
            var generated = await _repoAttendance.FirstOrDefaultAsync(new AttendanceByDateSpec(request.EmployeeId, date), cancellationToken);
            if (generated is not null)
            {
                var updatedAttendance = generated.UpdateEmployeeName(employee.NameFullInitial());
                await _repoAttendance.UpdateAsync(updatedAttendance, cancellationToken);
            }
            else
            {
                string day = date.DayOfWeek.ToString().ToUpper();
                var scheduleDetail = await _repoScheduleDetail.FirstOrDefaultAsync(new ScheduleDetailByEmployeeScheduleSpec(employee.ScheduleId, day), cancellationToken);

                if (scheduleDetail is null)
                {
                    continue;
                }

                string? description = null;
                string? notes = null;

                var scheduleDetailId = scheduleDetail.Id;
                var scheduleTimeIn1 = date + TimeSpan.Parse(scheduleDetail.TimeIn1);
                var scheduleTimeOut1 = date + TimeSpan.Parse(scheduleDetail.TimeOut1);
                var scheduleTimeIn2 = date + TimeSpan.Parse(scheduleDetail.TimeIn2);
                var scheduleTimeOut2 = date + TimeSpan.Parse(scheduleDetail.TimeOut2);

                string dayType = scheduleDetail.ScheduleType;

                // Check if date is calendar
                var calendar = await _repoCalendar.FirstOrDefaultAsync(new CalendarByDateSpec(date), cancellationToken);
                if (calendar is not null)
                {
                    dayType = "HOLIDAY";
                    description = calendar.Name!;
                    notes = calendar.Description!;
                }

                var attendance = new Attendance(request.EmployeeId, employee.NameFullInitial(), schedule.Id, schedule.Name, dayType, date, scheduleDetailId, day, scheduleDetail.TotalHours, scheduleTimeIn1, scheduleTimeOut1, scheduleTimeIn2, scheduleTimeOut2, "PENDING", description, notes);
                await _repoAttendance.AddAsync(attendance, cancellationToken);
            }
        }

        return DefaultIdType.Empty;
    }
}