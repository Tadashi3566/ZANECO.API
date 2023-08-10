namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentDto : BaseDto, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
}