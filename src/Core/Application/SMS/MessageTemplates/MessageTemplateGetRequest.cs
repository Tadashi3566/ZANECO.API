using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateGetRequest : IRequest<MessageTemplateDto>
{
    public Guid Id { get; set; }

    public MessageTemplateGetRequest(Guid id) => Id = id;
}

public class MessageTemplateGetRequestHandler : IRequestHandler<MessageTemplateGetRequest, MessageTemplateDto>
{
    private readonly IRepositoryWithEvents<MessageTemplate> _repository;
    private readonly IStringLocalizer<MessageTemplateGetRequestHandler> _localizer;

    public MessageTemplateGetRequestHandler(IRepositoryWithEvents<MessageTemplate> repository, IStringLocalizer<MessageTemplateGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageTemplateDto> Handle(MessageTemplateGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new MessageTemplateByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"MessageTemplate {request.Id} not found.");
}