using Mapster;
using ZANECO.API.Application.SMS.MessageIns;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS;

public class MessagesViaDapperRequest : IRequest<MessageInDto>
{
    public int Id { get; set; }

    public MessagesViaDapperRequest(int id) => Id = id;
}

public class MessageInViaDapperGetRequestHandler : IRequestHandler<MessagesViaDapperRequest, MessageInDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<MessageInViaDapperGetRequestHandler> _localizer;

    public MessageInViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<MessageInViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageInDto> Handle(MessagesViaDapperRequest request, CancellationToken cancellationToken)
    {
        var messageIn = await _repository.QueryFirstOrDefaultAsync<MessageIn>(
            $"SELECT * FROM datazaneco.\"MessageIns\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = messageIn ?? throw new NotFoundException($"Message {request.Id} not found.");

        return messageIn.Adapt<MessageInDto>();
    }
}