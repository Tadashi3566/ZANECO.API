using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public sealed class AppointmentByIdSpec : Specification<Appointment, AppointmentDto>, ISingleResultSpecification<Appointment>
{
    public AppointmentByIdSpec(int id) => Query.Where(p => p.Id == id);
}