using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentCreateRequest : RequestWithImageExtension, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
}

public class DependentCreateRequestValidator : CustomValidator<DependentCreateRequest>
{
    public DependentCreateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<DependentCreateRequestValidator> localizer)
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

public class DependentCreateRequestHandler : IRequestHandler<DependentCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Dependent> _repoDependent;
    private readonly IFileStorageService _file;

    public DependentCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Dependent> repoDependent, IFileStorageService file) =>
        (_repoEmployee, _repoDependent, _file) = (repoEmployee, repoDependent, file);

    public async Task<Guid> Handle(DependentCreateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        string imagePath = await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken);

        var dependent = new Dependent(request.EmployeeId, employee!.NameFull(), request.Name, request.Gender, request.BirthDate, request.Relation, request.Description, request.Notes, imagePath);

        await _repoDependent.AddAsync(dependent, cancellationToken);

        return dependent.Id;
    }
}