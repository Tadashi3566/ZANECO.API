namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public interface IAttendanceService
{
    public Task Generate(DefaultIdType employeeId, string employeeName, DateTime date, DefaultIdType scheduleId, string scheduleName, string dayType, DefaultIdType scheduleDetailId, string day, int hours, DateTime scheduleTimeIn1, DateTime scheduleTimeOut1, DateTime scheduleTimeIn2, DateTime scheduleTimeOut2, string description, string notes, CancellationToken cancellationToken);
}