using Mapster;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogGetViaDapperRequest : IRequest<MessageLogDto>
{
    public int Id { get; set; }

    public MessageLogGetViaDapperRequest(int id) => Id = id;
}

public class MessageLogViaDapperGetRequestHandler : IRequestHandler<MessageLogGetViaDapperRequest, MessageLogDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<MessageLogViaDapperGetRequestHandler> _localizer;

    public MessageLogViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<MessageLogViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageLogDto> Handle(MessageLogGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var messageLog = await _repository.QueryFirstOrDefaultAsync<MessageLog>(
            $"SELECT * FROM datazaneco.\"MessageLogs\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = messageLog ?? throw new NotFoundException(string.Format(_localizer["Message not found."], request.Id));

        return messageLog.Adapt<MessageLogDto>();
    }
}