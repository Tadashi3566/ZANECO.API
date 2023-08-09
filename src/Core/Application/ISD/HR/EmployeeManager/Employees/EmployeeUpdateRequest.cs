using ZANECO.API.Application.SMS;
using ZANECO.API.Application.SMS.Contacts;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeUpdateRequest : RequestWithImageExtension<EmployeeUpdateRequest>, IRequest<Guid>
{
    // Basic
    public bool IsActive { get; set; } = default!;

    public int Number { get; set; } = default!;
    public string Title { get; set; } = "MR";
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public string? Extension { get; set; }
    public string Gender { get; set; } = "MALE";
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? CivilStatus { get; set; }
    public string? Address { get; set; }
    public string? BirthPlace { get; set; }

    // Employment
    public DefaultIdType DesignationId { get; set; } = default!;

    public DateTime BirthDate { get; set; } = default!;
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
    public int DaysPerMonth { get; set; } = 22;
    public decimal RatePerDay { get; set; } = default!;
    public int HoursPerDay { get; set; } = 8;
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
}

public class EmployeeUpdateRequestValidator : CustomValidator<EmployeeUpdateRequest>
{
    public EmployeeUpdateRequestValidator()
    {
        RuleFor(p => p.Number)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(p => p.FirstName)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.LastName)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Gender)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(p => p.BirthDate)
            .NotNull();

        RuleFor(p => p.Email)
            .EmailAddress()
            .Unless(p => string.IsNullOrWhiteSpace(p.Email));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class EmployeeUpdateRequestHandler : IRequestHandler<EmployeeUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repoContact;
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IStringLocalizer<EmployeeUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public EmployeeUpdateRequestHandler(IRepositoryWithEvents<Contact> repoContact, IRepositoryWithEvents<Employee> repository, IStringLocalizer<EmployeeUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoContact, _repoEmployee, _localizer, _file) = (repoContact, repository, localizer, file);

    public async Task<Guid> Handle(EmployeeUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.Id, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentEmployeeImagePath = employee.ImagePath;
            if (!string.IsNullOrEmpty(currentEmployeeImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentEmployeeImagePath));
            }

            employee = employee.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Employee>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedEmployee = employee.Update(request.Number, request.Title, request.FirstName, request.MiddleName, request.LastName, request.Extension, request.Gender, ClassSms.FormatContactNumber(request.PhoneNumber!), request.Email!, request.CivilStatus!, request.Address!, request.BirthDate!, request.BirthPlace!, request.HireDate!, request.RegularDate!, request.Sss!, request.Phic!, request.Hdmf!, request.Tin!, request.EmergencyPerson!, request.EmergencyNumber!, request.EmergencyAddress!, request.EmergencyRelation!, request.FatherName!, request.MotherName!, request.Education!, request.Course!, request.Award!, request.BloodType!, request.Description, request.Notes, imagePath);

        await _repoEmployee.UpdateAsync(updatedEmployee, cancellationToken);

        // Handle PhoneNumber DocumentDate
        if (request.PhoneNumber.Length > 0)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                await _file.UploadAsync<Contact>(request.Image, FileType.Image, cancellationToken);
            }

            var contact = await _repoContact.FirstOrDefaultAsync(new ContactByNumberSpec(ClassSms.FormatContactNumber(request.PhoneNumber)), cancellationToken);
            if (contact is null)
            {
                var newContact = new Contact("EMPLOYEE", request.Number.ToString(), ClassSms.FormatContactNumber(request.PhoneNumber), employee.FullInitialName(), request.Address!, string.Empty, string.Empty, imagePath);
                await _repoContact.AddAsync(newContact, cancellationToken);
            }
            else
            {
                var updatedContact = contact.Update("EMPLOYEE", employee.Number.ToString(), ClassSms.FormatContactNumber(request.PhoneNumber), employee.FullInitialName(), request.Address!, string.Empty, string.Empty, imagePath);
                await _repoContact.UpdateAsync(updatedContact, cancellationToken);
            }
        }

        return request.Id;
    }
}