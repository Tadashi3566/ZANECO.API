namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeMobileDto : BaseDto, IDto
{
    // Basic
    public bool IsActive { get; set; } = default!;

    public int Number { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public string? Extension { get; set; }
    public string Gender { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    // Employment
    public DateTime? BirthDate { get; set; } = default!;

    public DateTime HireDate { get; set; } = default!;

    public string? Area { get; set; }
    public string? Department { get; set; }
    public string? Division { get; set; }
    public string? Section { get; set; }
    public string? Position { get; set; }

    public string FullName
    {
        get
        {
            if (Extension?.Length > 0)
                return $"{FirstName} {MiddleName} {LastName} {Extension}".Trim();
            else
                return $"{FirstName} {MiddleName} {LastName}".Trim();
        }
    }

    public string FullInitialName
    {
        get
        {
            if (MiddleName?.Length > 0)
                return $"{FirstName} {MiddleName[..1]}. {LastName} {Extension}".Trim();
            else
                return $"{FirstName} {LastName} {Extension}".Trim();
        }
    }

    public string NameFull
    {
        get
        {
            if (Extension?.Length > 0)
                return $"{LastName} {Extension}, {FirstName} {MiddleName}".Trim();
            else
                return $"{LastName}, {FirstName} {MiddleName}".Trim();
        }
    }

    public string NameFullInitial
    {
        get
        {
            if (MiddleName?.Length > 0)
                return $"{LastName}, {FirstName} {MiddleName[..1]}.".Trim();
            else
                return $"{LastName}, {FirstName}".Trim();
        }
    }
}