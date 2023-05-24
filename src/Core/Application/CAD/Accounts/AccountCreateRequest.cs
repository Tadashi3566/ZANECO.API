using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountCreateRequest : IRequest<Guid>
{
    public int IdCode { get; set; } = default!;
    public string AccountNumber { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public string Cipher { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string AccountType { get; set; } = string.Empty; // Residential, High and Low Voltage
    public string Feeder { get; set; } = string.Empty;
    public string Pole { get; set; } = string.Empty;
    public string Transformer { get; set; } = string.Empty;
    public string MeterBrand { get; set; } = string.Empty;
    public string MeterSerial { get; set; } = string.Empty;

    public string ConnectionStatus { get; set; } = string.Empty; // Active, Disconnected etc.

    public DateTime ConnectionDate { get; set; } = default!;
    public DateTime DisconnectionDate { get; set; } = default!;
    public DateTime ReconnectionDate { get; set; } = default!;

    public string BillMonth { get; set; } = string.Empty;
    public DateTime PreviousReadingDate { get; set; } = default!;
    public double PreviousReadingKWH { get; set; } = default!;
    public DateTime PresentReadingDate { get; set; } = default!;
    public double PresentReadingKWH { get; set; } = default!;
    public double UsedKWH { get; set; } = default!;
    public int Multiplier { get; set; }
    public decimal BillAmount { get; set; } = default!;
    public string BillNumber { get; set; } = string.Empty;

    public bool ChangedMeter { get; set; }
    public double PreviousReadingKWHCM { get; set; }
    public double PresentReadingKWHCM { get; set; }
    public double UsedKWHCM { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateAccountRequestValidator : CustomValidator<AccountCreateRequest>
{
    public CreateAccountRequestValidator(IReadRepository<Account> AccountRepo, IStringLocalizer<CreateAccountRequestValidator> localizer)
    {
        RuleFor(p => p.AccountNumber)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (account, ct) => await AccountRepo.FirstOrDefaultAsync(new AccountByAccountNumberSpec(account), ct) is null)
            .WithMessage((_, account) => string.Format(localizer["account already exists"], account));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await AccountRepo.FirstOrDefaultAsync(new AccountByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["account already exists"], name));

        RuleFor(p => p.Address)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class AccountCreateRequestHandler : IRequestHandler<AccountCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Account> _repository;
    private readonly IFileStorageService _file;

    public AccountCreateRequestHandler(IRepositoryWithEvents<Account> repository, IFileStorageService file) => (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(AccountCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Account>(request.Image, FileType.Image, cancellationToken);

        var account = new Account(request.IdCode, request.AccountNumber, request.Area, request.Route, request.Cipher, request.Tag, request.Name, request.Address, request.AccountType, request.Feeder, request.Pole, request.Transformer, request.MeterBrand, request.MeterSerial, request.BillMonth, request.PreviousReadingDate, request.PreviousReadingKWH, request.PresentReadingDate, request.PresentReadingKWH, request.UsedKWH, request.BillAmount, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(account, cancellationToken);

        return account.Id;
    }
}