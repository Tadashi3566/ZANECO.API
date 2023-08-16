using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryUpdateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string MrCode { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public DateTime DateReceived { get; set; } = default!;
    public decimal Cost { get; set; } = default!;
}

public class InventoryUpdateRequestValidator : CustomValidator<InventoryUpdateRequest>
{
    public InventoryUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<InventoryUpdateRequestValidator> localizer)
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

public class InventoryUpdateRequestHandler : IRequestHandler<InventoryUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Inventory> _repository;
    private readonly IFileStorageService _file;

    public InventoryUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Inventory> repository, IFileStorageService file) =>
        (_repoEmployee, _repository, _file) = (repoEmployee, repository, file);

    public async Task<Guid> Handle(InventoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        var inventory = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = inventory ?? throw new NotFoundException($"Inventory {request.Id} not found.");

        // Remove old file if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentFilePath = inventory.ImagePath;
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentFilePath));
            }

            inventory = inventory.ClearFilePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Inventory>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedInventory = inventory.Update(employee.FullName(), request.MrCode, request.ItemCode, request.DateReceived, request.Name, request.Cost, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedInventory, cancellationToken);

        return request.Id;
    }
}