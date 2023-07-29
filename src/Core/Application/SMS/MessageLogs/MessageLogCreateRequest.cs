using System.Security.Cryptography;
using System.Text;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogCreateRequest : IRequest<int>
{
    public string Connector { get; set; } = default!;
    public string Gateway { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageFrom { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string MessageGuid { get; set; } = default!;
    public int MessageParts { get; set; }
    public int StatusCode { get; set; }
    public string StatusText { get; set; } = default!;
    public string ErrorCode { get; set; } = default!;
    public string ErrorText { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class MessageLogCreateRequestValidator : CustomValidator<MessageLogCreateRequest>
{
    public MessageLogCreateRequestValidator()
    {
        RuleFor(p => p.MessageType)
             .NotEmpty()
             .MaximumLength(32);

        RuleFor(p => p.MessageFrom)
             .NotEmpty()
             .MaximumLength(16);

        RuleFor(p => p.MessageTo)
             .NotEmpty()
             .MaximumLength(16);

        RuleFor(p => p.MessageText)
             .NotEmpty()
             .MaximumLength(1500);
    }
}

public class MessageLogCreateRequestHandler : IRequestHandler<MessageLogCreateRequest, int>
{
    private readonly IRepositoryWithEvents<MessageLog> _repository;

    public MessageLogCreateRequestHandler(IRepositoryWithEvents<MessageLog> repository) => _repository = repository;

    public async Task<int> Handle(MessageLogCreateRequest request, CancellationToken cancellationToken)
    {
        byte[] sentenceBytes = Encoding.UTF8.GetBytes(request.MessageText);
        string messageHash;

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] hashBytes = sha256Hash.ComputeHash(sentenceBytes);
            messageHash = Convert.ToBase64String(hashBytes);
        }

        var messageLog = new MessageLog(request.Connector, request.Gateway, request.MessageType, request.MessageFrom, request.MessageTo, request.MessageText, request.MessageGuid, request.MessageParts, request.StatusCode, request.StatusText, request.ErrorCode, request.ErrorText, messageHash, request.Description, request.Notes);

        await _repository.AddAsync(messageLog, cancellationToken);

        return messageLog.Id;
    }
}