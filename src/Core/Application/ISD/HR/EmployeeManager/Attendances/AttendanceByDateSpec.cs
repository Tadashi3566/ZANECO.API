using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public sealed class AttendanceByDateSpec : Specification<Attendance>, ISingleResultSpecification<Attendance>
{
    public AttendanceByDateSpec(DefaultIdType employeeId, DateTime date) =>
        Query.Where(p => p.EmployeeId == employeeId && p.AttendanceDate == date);
}