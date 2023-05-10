using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountMigrateAccountRequest : IRequest<Guid>
{
    public bool IsBackgroundJob { get; set; } = default!;
    public bool IsScheduled { get; set; } = default!;
}

public class AccountMigrateAccountRequestValidator : CustomValidator<AccountMigrateAccountRequest>
{
}

public class AccountMigrateAccountRequestHandler : IRequestHandler<AccountMigrateAccountRequest, Guid>
{
    private readonly IDapperRepository _repoDapper;
    private readonly IRepositoryWithEvents<Account> _repoAccount;
    private readonly IRepositoryWithEvents<Ledger> _repoLedger;
    private readonly IJobService _jobService;

    public AccountMigrateAccountRequestHandler(IDapperRepository repoDapper, IRepositoryWithEvents<Account> repoAccount, IRepositoryWithEvents<Ledger> repoLedger, IJobService jobService) =>
        (_repoDapper, _repoAccount, _repoLedger, _jobService) = (repoDapper, repoAccount, repoLedger, jobService);

    public async Task<Guid> Handle(AccountMigrateAccountRequest request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT AccountNumber, Reference, Name, OrNumber, Area, Book, CBook, Sequence, WRateCode, FeederNumber, PoleNumber, Transformer, MeterBrand, Serial, BillMonth, DisconnectionDate, ReconnectionDate, PreviousReadingDate, PreviousReadingKWH, PresentReadingDate, PresentReadingKWH, KilowattHour, TotalBill FROM dmo.master";

        var accounts = await _repoDapper.QueryListAsync<Master>(sql, cancellationToken: cancellationToken);

        foreach (var account in accounts)
        {
            account.BillMonth ??= string.Empty;
            account.Sequence ??= string.Empty;
            account.FeederNumber ??= string.Empty;
            account.Serial ??= string.Empty;

            if (await _repoAccount.FirstOrDefaultAsync(new AccountByAccountNumberSpec(account.AccountNumber!), cancellationToken) is null)
            {
                var newAccount = new Account(account.Code, account.AccountNumber!, account.Area!, account.Book!, account.CBook!, account.Sequence!, account.Name!, account.Address!, account.WRateCode!, account.FeederNumber!, account.PoleNumber!, account.Transformer!, account.MeterBrand!, account.Serial!, account.BillMonth!, account.PreviousReadingDate, account.PreviousReadingKWH, account.PresentReadingDate, account.PresentReadingKWH, account.KilowattHour, account.TotalBill, string.Empty, string.Empty, null);

                if (request.IsBackgroundJob)
                {
                    if (request.IsScheduled)
                    {
                        _jobService.Schedule(() => _repoAccount.AddAsync(newAccount, cancellationToken), TimeSpan.FromMinutes(10));
                    }
                    else
                    {
                        _jobService.Enqueue(() => _repoAccount.AddAsync(newAccount, cancellationToken));
                    }
                }
                else
                {
                    await _repoAccount.AddAsync(newAccount, cancellationToken);
                }
            }
            else
            {
                if (request.IsBackgroundJob)
                {
                    if (request.IsScheduled)
                    {
                        _jobService.Schedule(() => AccountUpdate(account, cancellationToken), TimeSpan.FromMinutes(10));
                    }
                    else
                    {
                        _jobService.Enqueue(() => AccountUpdate(account, cancellationToken));
                    }
                }
                else
                {
                    await AccountUpdate(account, cancellationToken);
                }
            }
        }

        return Guid.Empty;
    }

    public async Task AccountUpdate(Master account, CancellationToken cancellationToken)
    {
        var existingAccount = await _repoAccount.FirstOrDefaultAsync(new AccountByAccountNumberSpec(account.AccountNumber!), cancellationToken);

        if (existingAccount is not null)
        {
            var updatedAccount = existingAccount.Migrate(string.Empty, DateTime.Today, DateTime.Today, DateTime.Today, account.BillMonth!, account.PreviousReadingDate, account.PreviousReadingKWH, account.PresentReadingDate, account.PresentReadingKWH, account.KilowattHour, account.TotalBill);

            await _repoAccount.UpdateAsync(updatedAccount, cancellationToken);
        }
    }
}