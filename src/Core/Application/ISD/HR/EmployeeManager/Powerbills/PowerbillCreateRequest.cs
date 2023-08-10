using ZANECO.API.Application.CAD.Accounts;
using ZANECO.API.Domain.CAD;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillCreateRequest : RequestWithImageExtension, IRequest<Guid>
{
    public Guid EmployeeId { get; set; }

    public string Account { get; set; } = default!;
    public string Meter { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class PowerbillCreateRequestValidator : CustomValidator<PowerbillCreateRequest>
{
    public PowerbillCreateRequestValidator(IReadRepository<Employee> repoEmployee, IStringLocalizer<PowerbillCreateRequestValidator> localizer)
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

public class PowerbillCreateRequestHandler : IRequestHandler<PowerbillCreateRequest, Guid>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IReadRepository<Account> _repoAccount;
    private readonly IRepositoryWithEvents<Powerbill> _repoPowerBill;
    private readonly IFileStorageService _file;

    public PowerbillCreateRequestHandler(IReadRepository<Employee> repoEmployee, IReadRepository<Account> repoAccount, IRepositoryWithEvents<Powerbill> repoPowerBill, IFileStorageService file) =>
        (_repoEmployee, _repoAccount, _repoPowerBill, _file) = (repoEmployee, repoAccount, repoPowerBill, file);

    public async Task<Guid> Handle(PowerbillCreateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException($"Employee {request.EmployeeId} not found.");

        if (!employee.IsActive) throw new Exception($"Employee {request.EmployeeId} is no longer Active");

        string imagePath = await _file.UploadAsync<Designation>(request.Image, FileType.Image, cancellationToken);

        // Get Account Information
        var account = await _repoAccount.FirstOrDefaultAsync(new AccountByAccountNumberSpec(request.Account), cancellationToken);
        _ = account ?? throw new NotFoundException($"Account {request.Account} not found.");

        var powerbill = new Powerbill(request.EmployeeId, employee.NameFull(), request.Account, account.MeterSerial, account.Name, account.Address, request.Description, request.Notes, imagePath);

        await _repoPowerBill.AddAsync(powerbill, cancellationToken);

        return powerbill.Id;
    }
}