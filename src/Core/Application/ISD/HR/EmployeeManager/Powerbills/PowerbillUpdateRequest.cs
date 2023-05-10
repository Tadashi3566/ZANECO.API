using ZANECO.API.Application.CAD.Accounts;
using ZANECO.API.Domain.CAD;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Account { get; set; } = default!;
    public string Meter { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class PowerbillUpdateRequestValidator : CustomValidator<PowerbillUpdateRequest>
{
    public PowerbillUpdateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<PowerbillUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoEmployee.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["employee not found."], id));

        RuleFor(p => p.Account)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class PowerbillUpdateRequestHandler : IRequestHandler<PowerbillUpdateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Account> _repoAccount;
    private readonly IRepositoryWithEvents<Powerbill> _repoPowerBill;
    private readonly IStringLocalizer<PowerbillUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public PowerbillUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Account> repoAccount, IRepositoryWithEvents<Powerbill> repoPowerBill, IStringLocalizer<PowerbillUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoEmployee, _repoAccount, _repoPowerBill, _localizer, _file) = (repoEmployee, repoAccount, repoPowerBill, localizer, file);

    public async Task<Guid> Handle(PowerbillUpdateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");

        if (!employee.IsActive) throw new Exception("Employee is no longer Active");

        // Get Account Information
        var account = await _repoAccount.FirstOrDefaultAsync(new AccountByAccountNumberSpec(request.Account), cancellationToken);
        _ = account ?? throw new NotFoundException("Account not found.");

        var powerbill = await _repoPowerBill.GetByIdAsync(request.Id, cancellationToken);

        _ = powerbill ?? throw new NotFoundException(string.Format(_localizer["powerbill not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = powerbill.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            powerbill = powerbill.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedPowerbill = powerbill.Update(request.EmployeeId, employee.NameFull(), request.Account, account.MeterSerial, account.Name, account.Address, request.Description, request.Notes, imagePath);

        await _repoPowerBill.UpdateAsync(updatedPowerbill, cancellationToken);

        return request.Id;
    }
}