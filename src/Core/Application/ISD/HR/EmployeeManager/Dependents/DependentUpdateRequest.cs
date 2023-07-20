using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class DependentUpdateRequestValidator : CustomValidator<DependentUpdateRequest>
{
    public DependentUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DependentUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Gender)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(p => p.Relation)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class DependentUpdateRequestHandler : IRequestHandler<DependentUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Dependent> _repoDependent;
    private readonly IStringLocalizer<DependentUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public DependentUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Dependent> repoDependent, IStringLocalizer<DependentUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoDependent, _localizer, _file) = (repoEmployee, repoDependent, localizer, file);

    public async Task<Guid> Handle(DependentUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        var dependent = await _repoDependent.GetByIdAsync(request.Id, cancellationToken);
        _ = dependent ?? throw new NotFoundException($"dependent {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = dependent.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            dependent = dependent.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedDependent = dependent.Update(request.EmployeeId, employee.NameFull(), request.Name, request.Gender, request.BirthDate, request.Relation, request.Description, request.Notes, imagePath);

        await _repoDependent.UpdateAsync(updatedDependent, cancellationToken);

        return request.Id;
    }
}