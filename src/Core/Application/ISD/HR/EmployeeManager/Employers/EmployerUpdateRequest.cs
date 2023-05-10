using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class EmployerUpdateRequestValidator : CustomValidator<EmployerUpdateRequest>
{
    public EmployerUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<EmployerUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Address)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Designation)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class EmployerUpdateRequestHandler : IRequestHandler<EmployerUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Employer> _repoEmployer;
    private readonly IStringLocalizer<EmployerUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public EmployerUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Employer> repoEmployer, IStringLocalizer<EmployerUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoEmployer, _localizer, _file) = (repoEmployee, repoEmployer, localizer, file);

    public async Task<Guid> Handle(EmployerUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");

        if (!employee.IsActive) throw new Exception("Employee is no longer Active");

        var employer = await _repoEmployer.GetByIdAsync(request.Id, cancellationToken);

        _ = employer ?? throw new NotFoundException(string.Format(_localizer["employer not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = employer.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            employer = employer.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedEmployer = employer.Update(request.EmployeeId, employee.NameFull(), request.Name, request.Address, request.Designation, request.StartDate, request.EndDate, request.Description, request.Notes, imagePath);

        await _repoEmployer.UpdateAsync(updatedEmployer, cancellationToken);

        return request.Id;
    }
}