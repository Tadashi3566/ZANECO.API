using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceByDateRangeSpec : Specification<Attendance, AttendanceDto>
{
    public AttendanceByDateRangeSpec(DefaultIdType employeeId, DateTime startDate, DateTime endDate) =>
        Query.Where(p => p.EmployeeId == employeeId && p.AttendanceDate >= startDate && p.AttendanceDate <= endDate);
}