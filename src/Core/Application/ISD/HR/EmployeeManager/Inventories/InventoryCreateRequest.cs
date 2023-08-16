using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryCreateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string MrCode { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public DateTime DateReceived { get; set; } = default!;
    public decimal Cost { get; set; } = default!;
}

public class CreateInventoryRequestValidator : CustomValidator<InventoryCreateRequest>
{
    public CreateInventoryRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();

        RuleFor(p => p.MrCode)
            .NotEmpty();

        RuleFor(p => p.ItemCode)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class InventoryCreateRequestHandler : IRequestHandler<InventoryCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Inventory> _repository;
    private readonly IFileStorageService _file;

    public InventoryCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Inventory> repository, IFileStorageService file) =>
        (_repoEmployee, _repository, _file) = (repoEmployee, repository, file);

    public async Task<Guid> Handle(InventoryCreateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        string imagePath = await _file.UploadAsync<Inventory>(request.Image, FileType.Image, cancellationToken);

        var inventory = new Inventory(request.EmployeeId, employee.FullName(), request.MrCode, request.ItemCode, request.DateReceived, request.Name, request.Cost, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(inventory, cancellationToken);

        return inventory.Id;
    }
}