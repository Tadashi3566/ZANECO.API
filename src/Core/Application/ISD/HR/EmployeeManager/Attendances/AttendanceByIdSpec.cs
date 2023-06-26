using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public sealed class AttendanceByIdSpec : Specification<Attendance, AttendanceDto>, ISingleResultSpecification
{
    public AttendanceByIdSpec(DefaultIdType id) => Query.Where(p => p.Id == id);
}