using ZANECO.API.Application.CAD.Ledgers;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountMigrateLedgerRequest : IRequest<Guid>
{
    public string AccountNumber { get; set; } = default!;
    public bool IsBackgroundJob { get; set; } = default!;
    public bool IsScheduled { get; set; } = default!;
}

public class AccountMigrateLedgerRequestValidator : CustomValidator<AccountMigrateLedgerRequest>
{
}

public class AccountMigrateLedgerRequestHandler : IRequestHandler<AccountMigrateLedgerRequest, Guid>
{
    private readonly IDapperRepository _repoDapper;
    private readonly IReadRepository<Account> _repoAccount;
    private readonly IRepositoryWithEvents<Ledger> _repoLedger;
    private readonly IJobService _jobService;

    public AccountMigrateLedgerRequestHandler(IDapperRepository repoDapper, IReadRepository<Account> repoAccount, IRepositoryWithEvents<Ledger> repoLedger, IJobService jobService) =>
        (_repoDapper, _repoAccount, _repoLedger, _jobService) = (repoDapper, repoAccount, repoLedger, jobService);

    public async Task<Guid> Handle(AccountMigrateLedgerRequest request, CancellationToken cancellationToken)
    {
        if (request.IsBackgroundJob)
        {
            if (request.IsScheduled)
            {
                _jobService.Schedule(() => LedgerMigrate(request.AccountNumber, cancellationToken), TimeSpan.FromMinutes(1));
            }
            else
            {
                _jobService.Enqueue(() => LedgerMigrate(request.AccountNumber, cancellationToken));
            }
        }
        else
        {
            await LedgerMigrate(request.AccountNumber, cancellationToken);
        }

        return Guid.Empty;
    }

    public async Task LedgerMigrate(string accountNumber, CancellationToken cancellationToken)
    {
        var account = await _repoAccount.FirstOrDefaultAsync(new AccountByAccountNumberSpec(accountNumber), cancellationToken);

        if (account is not null)
        {
            //var existingLedger = await _repoDapper.QueryListAsync<Ledger>($"SELECT AccountNumber FROM datazaneco.ledger WHERE AccountNumber = '{account.AccountNumber}'", cancellationToken: cancellationToken);
            int ledgerCount = await _repoLedger.CountAsync(new AccountCountSpec(account.IdCode), cancellationToken);
            var existingARLedgers = await _repoDapper.QueryListAsync<ARLedger>($"SELECT AccountNumber FROM dmo.arledger WHERE AccountNumber = '{account.IdCode}'", cancellationToken: cancellationToken);

            if (ledgerCount != existingARLedgers.Count)
            {
                var arLedgers = await _repoDapper.QueryListAsync<ARLedger>($"SELECT * FROM dmo.arledger WHERE AccountNumber = '{account.IdCode}'", cancellationToken: cancellationToken);

                foreach (var ledger in arLedgers)
                {
                    //var existingLedger = await _repoDapper.QueryFirstOrDefaultAsync<Ledger>($"SELECT AccountNumber, BillNumber FROM ledgers WHERE AccountNumber = '{ledger.AccountNumber}' AND BillNumber = '{ledger.BillNumber}'", cancellationToken: cancellationToken);
                    bool existingLedger = await _repoLedger.AnyAsync(new LedgerByExistingLedgerSpec(ledger.IdCode, ledger.BillNumber!, ledger.BillMonth!), cancellationToken);

                    if (!existingLedger)
                    {
                        ledger.BillNumber ??= string.Empty;
                        ledger.BillMonth ??= string.Empty;
                        ledger.Collector ??= string.Empty;

                        var newLedger = new Ledger(account.Id, account.IdCode, account.AccountNumber, ledger.BillNumber, ledger.BillMonth, ledger.LastReading, ledger.KWH, ledger.UCNPCSD, ledger.UCNPCSCC, ledger.UCDUSCC, ledger.UCME, ledger.UCETR, ledger.UCEC, ledger.UCCSR, ledger.VATDistribution, ledger.VATGeneration, ledger.VATTransmission, ledger.VATSLGeneration, ledger.VATSLTransmission, ledger.VAT, ledger.VATDiscount, ledger.Debit, ledger.Credit, ledger.Balance, ledger.Collector, ledger.PostingDate);

                        await _repoLedger.AddAsync(newLedger, cancellationToken);
                    }
                }
            }
        }
    }
}