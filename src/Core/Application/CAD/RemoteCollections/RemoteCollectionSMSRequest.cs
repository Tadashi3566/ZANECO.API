using ZANECO.API.Application.SMS;
using ZANECO.API.Application.SMS.Contacts;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionSMSRequest : IRequest<Guid>
{
    public string Collector { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
}

public class RemoteCollectionSMSRequestValidator : CustomValidator<RemoteCollectionSMSRequest>
{
    public RemoteCollectionSMSRequestValidator()
    {
        RuleFor(p => p.AccountNumber)
            .NotEmpty()
            .MaximumLength(10);
    }
}

public class RemoteCollectionSMSRequestHandler : IRequestHandler<RemoteCollectionSMSRequest, Guid>
{
    private readonly IReadRepository<Contact> _repoContact;
    private readonly IJobService _jobService;
    private readonly ISmsService _smsService;

    public RemoteCollectionSMSRequestHandler(IReadRepository<Contact> repoContact, IJobService jobService, ISmsService smsService) =>
        (_repoContact, _jobService, _smsService) = (repoContact, jobService, smsService);

    public async Task<Guid> Handle(RemoteCollectionSMSRequest request, CancellationToken cancellationToken)
    {
        var contacts = await _repoContact.ListAsync(new ContactsByAccountSpec(request.AccountNumber), cancellationToken);

        if (contacts.Count > 0)
        {
            //string message = $"We received your ZANECO Electric Bill with an amount of Php. {request.Amount} paid on {request.TransactionDate:D} via {request.Collector} Ref# {request.Reference} and successfully posted to our System.";
            //string message = $"Thank you for your payment of Php. {request.Amount:N2} for ZANECO Account {request.AccountNumber} made on {request.TransactionDate:D} through {request.Collector} Ref#{request.Reference} and succesfully posted to our system.";
            const string message = "Sorry for the wrong information that we sent through SMS. It was not our intention to send wrong amount that has caused any inconvenience.";

            foreach (var contact in contacts!)
            {
                _jobService.Enqueue(() => _smsService.SmsSend(ClassSms.FormatContactNumber(contact!.PhoneNumber), message, true, "sms.automatic"));
            }
        }

        return Guid.Empty;
    }
}