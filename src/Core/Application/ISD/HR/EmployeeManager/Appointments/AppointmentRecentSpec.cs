using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public sealed class AppointmentRecentSpec : Specification<Appointment, AppointmentDto>, ISingleResultSpecification<Appointment>
{
    public AppointmentRecentSpec(DefaultIdType employeeId) =>
        Query.Where(p => p.EmployeeId.Equals(employeeId))
            .OrderByDescending(p => p.CreatedOn)
            .Take(1);
}