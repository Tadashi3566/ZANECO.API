using Mapster;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogGetMessageViaDapperRequest : IRequest<MessageLogDto>
{
    public string MessageTo { get; set; }
    public string MessageText { get; set; }

    public MessageLogGetMessageViaDapperRequest(string messageTo, string messageText) => (MessageTo, MessageText) = (messageTo, messageText);
}

public class MessageLogGetMessageViaDapperRequestHandler : IRequestHandler<MessageLogGetMessageViaDapperRequest, MessageLogDto>
{
    private readonly IDapperRepository _repository;

    public MessageLogGetMessageViaDapperRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<MessageLogDto> Handle(MessageLogGetMessageViaDapperRequest request, CancellationToken cancellationToken)
    {
        var messageLog = await _repository.QueryFirstOrDefaultAsync<MessageLog>(
            $"SELECT MessageTo, MessageText FROM datazaneco.MessageLogs WHERE MessageTo = '{request.MessageTo}' AND MessageText = '{request.MessageText}'", cancellationToken: cancellationToken);

        _ = messageLog ?? throw new NotFoundException("Message not found.");

        return messageLog.Adapt<MessageLogDto>();
    }
}