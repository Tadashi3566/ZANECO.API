using ZANECO.API.Domain.AGMA;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactMigrateRequest : IRequest<Guid>
{
}

public class ContactMigrateRequestValidator : CustomValidator<ContactMigrateRequest>
{
}

public class ContactMigrateRequestHandler : IRequestHandler<ContactMigrateRequest, Guid>
{
    private readonly IDapperRepository _repoDapper;
    private readonly IRepositoryWithEvents<Contact> _repository;

    public ContactMigrateRequestHandler(IDapperRepository repoDapper, IRepositoryWithEvents<Contact> repository) =>
        (_repoDapper, _repository) = (repoDapper, repository);

    public async Task<Guid> Handle(ContactMigrateRequest request, CancellationToken cancellationToken)
    {
        var accounts = await _repoDapper.QueryListAsync<Master2022>($"SELECT Reference, Name, address, contact_number, District, is_registered FROM dmo.master_2022 WHERE is_registered = 1", cancellationToken: cancellationToken);

        foreach (var account in accounts)
        {
            if (await _repository.FirstOrDefaultAsync(new ContactByAccountSpec(account.AccountNumber!), cancellationToken) is null)
            {
                var contact = new Contact("ACCOUNT", account.AccountNumber!, ClassSms.FormatContactNumber(account.contact_number!), account.Name!, account.address!, account.District, string.Empty, string.Empty);

                await _repository.AddAsync(contact, cancellationToken);
            }
        }

        return Guid.Empty;
    }
}