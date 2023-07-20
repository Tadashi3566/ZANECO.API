using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class DeleteMessageTemplateRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteMessageTemplateRequest(Guid id) => Id = id;
}

public class MessageTemplateDeleteRequestHandler : IRequestHandler<DeleteMessageTemplateRequest, Guid>
{
    private readonly IRepositoryWithEvents<MessageTemplate> _repository;
    private readonly IStringLocalizer<MessageTemplateDeleteRequestHandler> _localizer;

    public MessageTemplateDeleteRequestHandler(IRepositoryWithEvents<MessageTemplate> repository, IStringLocalizer<MessageTemplateDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteMessageTemplateRequest request, CancellationToken cancellationToken)
    {
        var messageTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = messageTemplate ?? throw new NotFoundException($"MessageTemplate {request.Id} not found.");

        await _repository.DeleteAsync(messageTemplate, cancellationToken);

        return request.Id;
    }
}