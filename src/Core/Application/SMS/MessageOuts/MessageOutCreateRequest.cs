using System.Text.RegularExpressions;
using ZANECO.API.Application.SMS.Contacts;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutCreateRequest : IRequest<int>
{
    public bool IsBackgroundJob { get; set; } = default!;
    public bool IsScheduled { get; set; } = default!;
    public bool IsFollowUp { get; set; } = default!;
    public DateTime Schedule { get; set; } = default!;
    public bool IsAPI { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string? Description { get; set; }
}

public class MessageOutCreateRequestValidator : CustomValidator<MessageOutCreateRequest>
{
    public MessageOutCreateRequestValidator()
    {
        RuleFor(p => p.MessageType)
             .NotEmpty()
             .MaximumLength(32);

        RuleFor(p => p.MessageTo)
             .NotEmpty();

        RuleFor(p => p.Subject)
             .NotEmpty()
             .MaximumLength(256);

        RuleFor(p => p.MessageText)
             .NotEmpty()
             .MaximumLength(1500);
    }
}

public class MessageOutCreateRequestHandler : IRequestHandler<MessageOutCreateRequest, int>
{
    private readonly IReadRepository<Contact> _repoContact;
    private readonly ISmsService _smsService;
    private readonly IJobService _jobService;

    public MessageOutCreateRequestHandler(IReadRepository<Contact> repoContact, ISmsService smsService, IJobService jobService)
    {
        _repoContact = repoContact;
        _smsService = smsService;
        _jobService = jobService;
    }

    public async Task<int> Handle(MessageOutCreateRequest request, CancellationToken cancellationToken)
    {
        string messageText = request.MessageText;
        string[] recipientArray = ClassSms.GetDistinctFromArray(request.MessageTo);

        if (recipientArray.Length.Equals(0)) return 0;

        foreach (string recipient in recipientArray)
        {
            if (recipient.Length <= 9) continue;

            //Get AccountNumber and Amount here
            if (request.MessageText.Contains("{AccountNumber}") || request.MessageText.Contains("{Amount}"))
            {
                var contact = await _repoContact.SingleOrDefaultAsync(new ContactByNumberSpec(recipient), cancellationToken);
                if (contact is null) continue;

                string remarks = contact.Remarks.Trim();
                decimal amount = default;

                // Define a regular expression pattern to match numeric values with optional decimal points
                const string pattern = @"[-+]?\d+(\.\d+)?";

                // Create a regex match collection
                MatchCollection matches = Regex.Matches(remarks, pattern);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches.Cast<Match>())
                    {
                        amount = Convert.ToDecimal(match.Value);
                    }
                }

                messageText = request.MessageText;
                messageText = messageText.Replace("{Amount}", $"P{amount:N2}");
                messageText = messageText.Replace("{AccountNumber}", contact.Reference);
            }

            //return 0;

            if (request.IsBackgroundJob)
            {
                if (request.IsScheduled)
                {
                    _jobService.Schedule(
                        () => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), messageText, true, request.IsAPI, request.MessageType), TimeSpan.FromMinutes(1));

                    if (request.IsFollowUp)
                    {
                        // Only send the SMS Subject in order to decrease the cost of SMS Service
                        _jobService.Schedule(
                            () => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), request.Subject.Trim(), true, request.IsAPI, request.MessageType), request.Schedule.AddHours(-5));
                    }
                }
                else
                {
                    _jobService.Enqueue(
                        () => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), messageText, true, request.IsAPI, request.MessageType));
                }
            }
            else
            {
                await _smsService.SmsSend(recipient, messageText, true, request.IsAPI, request.MessageType)
                    .ConfigureAwait(false);
            }
        }

        return recipientArray.Length;
    }
}