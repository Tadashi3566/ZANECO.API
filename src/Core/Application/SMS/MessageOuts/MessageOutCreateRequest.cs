namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutCreateRequest : IRequest<int>
{
    public bool IsBackgroundJob { get; set; } = default!;
    public bool IsScheduled { get; set; } = default!;
    public DateTime Schedule { get; set; } = default!;
    public bool IsAPI { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
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
    private readonly ISmsService _smsService;
    private readonly IJobService _jobService;

    public MessageOutCreateRequestHandler(ISmsService smsService, IJobService jobService)
    {
        _smsService = smsService;
        _jobService = jobService;
    }

    public async Task<int> Handle(MessageOutCreateRequest request, CancellationToken cancellationToken)
    {
        string[] recipientArray = ClassSms.GetDistinctFromArray(request.MessageTo);

        if (recipientArray.Length.Equals(0)) return 0;

        foreach (string recipient in recipientArray)
        {
            if (recipient.Length <= 9) continue;

            if (request.IsBackgroundJob)
            {
                if (request.IsScheduled)
                {
                    _jobService.Schedule(() => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), request.MessageText.Trim(), request.IsAPI, request.MessageType), TimeSpan.FromMinutes(1));

                    // Only send the SMS Subject in order to decrease the cost of SMS Service
                    _jobService.Schedule(() => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), request.Subject.Trim(), request.IsAPI, request.MessageType), request.Schedule.AddHours(-5));
                }
                else
                {
                    _jobService.Enqueue(() => _smsService.SmsSend(ClassSms.FormatContactNumber(recipient), request.MessageText, request.IsAPI, request.MessageType));
                }
            }
            else
            {
                await _smsService.SmsSend(recipient, request.MessageText, request.IsAPI, request.MessageType).ConfigureAwait(false);
            }
        }

        return 1;
    }
}