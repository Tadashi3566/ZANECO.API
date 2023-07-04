using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogGetRequest : IRequest<MessageLogDto>
{
    public int Id { get; set; }

    public MessageLogGetRequest(int id) => Id = id;
}

public class MessageLogGetRequestHandler : IRequestHandler<MessageLogGetRequest, MessageLogDto>
{
    private readonly IRepositoryWithEvents<MessageLog> _repository;
    private readonly IStringLocalizer<MessageLogGetRequestHandler> _localizer;

    public MessageLogGetRequestHandler(IRepositoryWithEvents<MessageLog> repository, IStringLocalizer<MessageLogGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageLogDto> Handle(MessageLogGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new MessageLogByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Message not found."], request.Id));
}