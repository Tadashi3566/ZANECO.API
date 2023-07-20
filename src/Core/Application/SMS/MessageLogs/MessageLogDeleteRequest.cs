using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class DeleteMessageLogRequest : IRequest<int>
{
    public int Id { get; set; }

    public DeleteMessageLogRequest(int id) => Id = id;
}

public class MessageLogDeleteRequestHandler : IRequestHandler<DeleteMessageLogRequest, int>
{
    private readonly IRepositoryWithEvents<MessageLog> _repository;
    private readonly IStringLocalizer<MessageLogDeleteRequestHandler> _localizer;

    public MessageLogDeleteRequestHandler(IRepositoryWithEvents<MessageLog> repository, IStringLocalizer<MessageLogDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<int> Handle(DeleteMessageLogRequest request, CancellationToken cancellationToken)
    {
        var messageLog = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = messageLog ?? throw new NotFoundException($"Message {request.Id} not found.");

        await _repository.DeleteAsync(messageLog, cancellationToken);

        return request.Id;
    }
}