using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentByIdSpec : Specification<Appointment, AppointmentDto>, ISingleResultSpecification
{
    public AppointmentByIdSpec(int id) => Query.Where(p => p.Id == id);
}