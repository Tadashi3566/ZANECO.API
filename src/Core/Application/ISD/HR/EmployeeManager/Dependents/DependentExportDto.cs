﻿namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}