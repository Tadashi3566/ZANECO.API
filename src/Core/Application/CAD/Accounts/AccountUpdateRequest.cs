using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
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
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class AccountUpdateRequestValidator : CustomValidator<AccountUpdateRequest>
{
    public AccountUpdateRequestValidator(IReadRepository<Account> AccountRepo, IStringLocalizer<AccountUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.AccountNumber)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (account, accountNumber, ct) =>
                    await AccountRepo.FirstOrDefaultAsync(new AccountByNameSpec(accountNumber), ct)
                        is not { } existingAccount || existingAccount.Id == account.Id)
                .WithMessage((_, name) => string.Format(localizer["account already exists"], name));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        //.MustAsync(async (account, name, ct) =>
        //        await AccountRepo.FirstOrDefaultAsync(new AccountByNameSpec(name), ct)
        //            is not Account existingAccount || existingAccount.Id == account.Id)
        //    .WithMessage((_, name) => string.Format(localizer["account already exists"], name));

        RuleFor(p => p.Address)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class AccountUpdateRequestHandler : IRequestHandler<AccountUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Account> _repository;
    private readonly IStringLocalizer<AccountUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public AccountUpdateRequestHandler(IRepositoryWithEvents<Account> repository, IStringLocalizer<AccountUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(AccountUpdateRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = account ?? throw new NotFoundException(string.Format(_localizer["Account not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = account.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            account = account.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Account>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedAccount = account.Update(request.Area, request.Route, request.Cipher, request.Tag, request.Name, request.Address, request.AccountType, request.Feeder, request.Pole, request.Transformer, request.MeterBrand, request.MeterSerial, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedAccount, cancellationToken);

        return request.Id;
    }
}