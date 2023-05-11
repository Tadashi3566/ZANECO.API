namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeDto : IDto
{
    public DefaultIdType Id { get; set; }

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
    public string? CivilStatus { get; set; }
    public string? Address { get; set; }
    public string? BirthPlace { get; set; }

    // Employment
    public DefaultIdType DesignationId { get; set; } = default!;

    public DateTime? BirthDate { get; set; } = default!;
    public DateTime HireDate { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime RegularDate { get; set; } = default!;

    public string? Area { get; set; }
    public string? Department { get; set; }
    public string? Division { get; set; }
    public string? Section { get; set; }
    public string? Position { get; set; }

    // Benefits
    public string? Sss { get; set; }

    public string? Phic { get; set; }
    public string? Hdmf { get; set; }
    public string? Tin { get; set; }

    // Payroll
    public string? EmploymentType { get; set; }

    public int SalaryNumber { get; set; }
    public string? SalaryName { get; set; }
    public decimal SalaryAmount { get; set; } = default!;
    public string? RateType { get; set; }
    public int DaysPerMonth { get; set; } = default!;
    public decimal RatePerDay { get; set; } = default!;
    public int HoursPerDay { get; set; } = default!;
    public decimal RatePerHour { get; set; } = default!;
    public string? TaxType { get; set; }
    public string? PayType { get; set; }
    public string? PayThrough { get; set; }

    public DefaultIdType ScheduleId { get; set; } = default!;
    public string? ScheduleName { get; set; }

    // Emergency
    public string? EmergencyPerson { get; set; }

    public string? EmergencyAddress { get; set; }
    public string? EmergencyNumber { get; set; }
    public string? EmergencyRelation { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }

    // Education
    public string? Education { get; set; }

    public string? Course { get; set; }
    public string? Award { get; set; }

    // Others
    public string? BloodType { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? ImagePath { get; set; }

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