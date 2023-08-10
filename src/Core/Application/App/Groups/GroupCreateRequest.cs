using ZANECO.API.Domain.App;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.App.Groups;

public class GroupCreateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public string Application { get; set; } = default!;
    public string Parent { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public Guid EmployeeId { get; set; } = default!;
}

public class GroupCreateRequestValidator : CustomValidator<GroupCreateRequest>
{
    public GroupCreateRequestValidator(IReadRepository<Group> repoGroup, IStringLocalizer<GroupCreateRequestValidator> localizer)
    {
        RuleFor(p => p.Application)
             .NotEmpty()
             .MaximumLength(16);

        RuleFor(p => p.Parent)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Number)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(32)
            .MustAsync(async (code, ct) => await repoGroup.FirstOrDefaultAsync(new GroupByCodeSpec(code), ct) is null)
            .WithMessage((_, code) => string.Format(localizer["Group {0} already exists."], code));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (name, ct) => await repoGroup.FirstOrDefaultAsync(new GroupByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["Group already exists."], name));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class GroupCreateRequestHandler : IRequestHandler<GroupCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Group> _repository;
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IFileStorageService _file;

    public GroupCreateRequestHandler(IRepositoryWithEvents<Group> repository, IReadRepository<Employee> repoEmployee, IFileStorageService file) =>
        (_repository, _repoEmployee, _file) = (repository, repoEmployee, file);

    public async Task<Guid> Handle(GroupCreateRequest request, CancellationToken cancellationToken)
    {
        string? employeeName = null;

        if (request.EmployeeId != default)
        {
            var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee is not null)
            {
                employeeName = employee.FullName();
            }
        }

        string imagePath = await _file.UploadAsync<Group>(request.Image, FileType.Image, cancellationToken);

        var group = new Group(request.Application, request.Parent, request.Tag, request.Number, request.Code, request.Name, request.Amount, request.EmployeeId, employeeName, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(group, cancellationToken);

        return group.Id;
    }
}