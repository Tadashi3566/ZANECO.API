namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeExportDto : DtoExtension<EmployeeExportDto>, IDto
{
    public string Area { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Division { get; set; } = default!;
    public string Section { get; set; } = default!;
    public string Position { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
}