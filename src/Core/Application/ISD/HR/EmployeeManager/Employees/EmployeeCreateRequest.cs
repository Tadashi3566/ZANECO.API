using ZANECO.API.Application.SMS;
using ZANECO.API.Application.SMS.Contacts;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeCreateRequest : IRequest<Guid>
{
    public bool IsActive { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Title { get; set; } = "MR";
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public string? Extension { get; set; }

    public string Gender { get; set; } = "MALE";
    public DateTime BirthDate { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? CivilStatus { get; set; }
    public string? Address { get; set; }
    public string? BirthPlace { get; set; }

    // Benefits
    public string? Sss { get; set; }
    public string? Phic { get; set; }
    public string? Hdmf { get; set; }
    public string? Tin { get; set; }

    // Payroll
    public DateTime HireDate { get; set; } = default!;

    public DateTime RegularDate { get; set; } = default!;
    public string? PayThrough { get; set; }

    // Emergency
    public string? EmergencyPerson { get; set; }

    public string? EmergencyNumber { get; set; }
    public string? EmergencyAddress { get; set; }
    public string? EmergencyRelation { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }

    // Education
    public string? Education { get; set; }

    public string? Course { get; set; }
    public string? Award { get; set; }

    // Others
    public string? BloodType { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }

    public ImageUploadRequest? Image { get; set; }
}

public class EmployeeCreateRequestValidator : CustomValidator<EmployeeCreateRequest>
{
    public EmployeeCreateRequestValidator(IReadRepository<Employee> repository, IStringLocalizer<EmployeeCreateRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .NotEmpty()
            .GreaterThan(0)
            .MustAsync(async (number, ct) => await repository.FirstOrDefaultAsync(new EmployeeByNumberSpec(number), ct) is null)
            .WithMessage((_, number) => string.Format(localizer["Employee already exists."], number));

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

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class EmployeeCreateRequestHandler : IRequestHandler<EmployeeCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repoContact;
    private readonly IRepositoryWithEvents<Employee> _repoEmployee;
    private readonly IFileStorageService _file;

    public EmployeeCreateRequestHandler(IRepositoryWithEvents<Contact> repoContact, IRepositoryWithEvents<Employee> employeeRepository, IFileStorageService file) =>
        (_repoContact, _repoEmployee, _file) = (repoContact, employeeRepository, file);

    public async Task<Guid> Handle(EmployeeCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Employee>(request.Image, FileType.Image, cancellationToken);

        var employee = new Employee(request.Number, request.Title, request.FirstName, request.MiddleName, request.LastName, request.Extension, request.Gender, ClassSms.FormatContactNumber(request.PhoneNumber), request.Email, request.CivilStatus, request.Address, request.BirthDate, request.BirthPlace, request.HireDate, request.RegularDate, request.Sss, request.Phic, request.Hdmf, request.Tin, request.EmergencyPerson, request.EmergencyNumber, request.EmergencyAddress, request.EmergencyRelation, request.FatherName, request.MotherName, request.Education, request.Course, request.Award, request.BloodType, request.Description, request.Notes, imagePath);

        await _repoEmployee.AddAsync(employee, cancellationToken);

        //Add Employee Contact Number
        if (request.PhoneNumber?.Length > 0)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                await _file.UploadAsync<Contact>(request.Image, FileType.Image, cancellationToken);
            }

            var contact = await _repoContact.FirstOrDefaultAsync(new ContactByNumberSpec(ClassSms.FormatContactNumber(request.PhoneNumber)), cancellationToken);
            if (contact is null)
            {
                var newContact = new Contact("EMPLOYEE", request.Number.ToString(), ClassSms.FormatContactNumber(request.PhoneNumber), employee.FullInitialName(), request.Address, string.Empty, string.Empty, imagePath);
                await _repoContact.AddAsync(newContact, cancellationToken);
            }
            else
            {
                var updatedContact = contact.Update("EMPLOYEE", employee.Number.ToString(), ClassSms.FormatContactNumber(request.PhoneNumber), employee.FullInitialName(), request.Address, string.Empty, string.Empty, imagePath);
                await _repoContact.UpdateAsync(updatedContact!, cancellationToken);
            }
        }

        //Emergency Person Contact Number
        if (request.EmergencyNumber?.Length > 0)
        {
            var contact = await _repoContact.FirstOrDefaultAsync(new ContactByNumberSpec(ClassSms.FormatContactNumber(request.EmergencyNumber)), cancellationToken);
            if (contact is null)
            {
                var newContact = new Contact("EMERGENCY", request.Number.ToString(), ClassSms.FormatContactNumber(request.EmergencyNumber), request.EmergencyPerson, request.EmergencyAddress, string.Empty, string.Empty, string.Empty);
                await _repoContact.AddAsync(newContact, cancellationToken);
            }
            else
            {
                var updatedContact = contact.Update("EMERGENCY", employee.Number.ToString(), ClassSms.FormatContactNumber(request.EmergencyNumber), request.EmergencyPerson, request.EmergencyAddress, string.Empty, string.Empty, string.Empty);
                await _repoContact.UpdateAsync(updatedContact!, cancellationToken);
            }
        }

        return employee.Id;
    }
}