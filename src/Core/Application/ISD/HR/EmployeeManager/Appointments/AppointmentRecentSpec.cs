﻿using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentRecentSpec : Specification<Appointment, AppointmentDto>, ISingleResultSpecification
{
    public AppointmentRecentSpec(DefaultIdType employeeId) =>
        Query.Where(p => p.EmployeeId.Equals(employeeId))
        .OrderByDescending(p => p.CreatedOn)
        .Take(1);
}