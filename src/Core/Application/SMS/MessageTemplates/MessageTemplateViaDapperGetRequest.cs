using Mapster;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateGetViaDapperRequest : IRequest<MessageTemplateDto>
{
    public DefaultIdType Id { get; set; }

    public MessageTemplateGetViaDapperRequest(Guid id) => Id = id;
}

public class MessageTemplateViaDapperGetRequestHandler : IRequestHandler<MessageTemplateGetViaDapperRequest, MessageTemplateDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<MessageTemplateViaDapperGetRequestHandler> _localizer;

    public MessageTemplateViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<MessageTemplateViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageTemplateDto> Handle(MessageTemplateGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var messageTemplate = await _repository.QueryFirstOrDefaultAsync<MessageTemplate>(
            $"SELECT * FROM datazaneco.\"MessageTemplates\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = messageTemplate ?? throw new NotFoundException($"MessageTemplate {request.Id} not found.");

        return messageTemplate.Adapt<MessageTemplateDto>();
    }
}