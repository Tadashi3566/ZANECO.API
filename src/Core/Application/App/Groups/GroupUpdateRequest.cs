using ZANECO.API.Domain.App;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.App.Groups;

public class GroupUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public string Application { get; set; } = default!;
    public string Parent { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string Tag { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class GroupUpdateRequestValidator : CustomValidator<GroupUpdateRequest>
{
    public GroupUpdateRequestValidator(IReadRepository<Group> repoGroupRepo, IStringLocalizer<GroupUpdateRequestValidator> localizer)
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
            .MustAsync(async (group, code, ct) =>
                    await repoGroupRepo.FirstOrDefaultAsync(new GroupByCodeSpec(code), ct)
                        is not { } existingGroup || existingGroup.Id == group.Id)
                .WithMessage((_, code) => string.Format(localizer["group already exists"], code));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (group, name, ct) =>
                    await repoGroupRepo.FirstOrDefaultAsync(new GroupByNameSpec(name), ct)
                        is not { } existingGroup || existingGroup.Id == group.Id)
                .WithMessage((_, name) => string.Format(localizer["group already exists"], name));

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class GroupUpdateRequestHandler : IRequestHandler<GroupUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Group> _repository;
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IStringLocalizer<GroupUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public GroupUpdateRequestHandler(IRepositoryWithEvents<Group> repository, IReadRepository<Employee> repoEmployee, IStringLocalizer<GroupUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _repoEmployee, _localizer, _file) = (repository, repoEmployee, localizer, file);

    public async Task<Guid> Handle(GroupUpdateRequest request, CancellationToken cancellationToken)
    {
        var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = group ?? throw new NotFoundException(string.Format(_localizer["group not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = group.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            group = group.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Group>(request.Image, FileType.Image, cancellationToken)
            : null;

        Guid? employeeId = null;
        string? employeeName = null;

        if (request.EmployeeId != default)
        {
            var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
            if (employee is not null)
            {
                employeeId = employee.Id;
                employeeName = employee.TitleFullInitialName();
            }
        }

        var updatedGroup = group.Update(request.Application, request.Parent, request.Tag, request.Number, request.Code, request.Name, request.Amount, employeeId, employeeName, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedGroup, cancellationToken);

        return request.Id;
    }
}