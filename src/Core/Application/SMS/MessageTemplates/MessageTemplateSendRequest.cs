using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateSendRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
}

public class MessageTemplateSendRequestValidator : CustomValidator<MessageTemplateSendRequest>
{
    public MessageTemplateSendRequestValidator()
    {
        RuleFor(p => p.Id)
             .NotEmpty();
    }
}

public class MessageTemplateSendRequestHandler : IRequestHandler<MessageTemplateSendRequest, Guid>
{
    private readonly IRepositoryWithEvents<MessageTemplate> _repository;
    private readonly IStringLocalizer<MessageTemplateSendRequestHandler> _localizer;

    public MessageTemplateSendRequestHandler(IRepositoryWithEvents<MessageTemplate> repository, IStringLocalizer<MessageTemplateSendRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(MessageTemplateSendRequest request, CancellationToken cancellationToken)
    {
        var messageTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = messageTemplate ?? throw new NotFoundException(string.Format(_localizer["MessageTemplate not found."], request.Id));

        var sendMessageTemplate = messageTemplate.Sent();

        await _repository.UpdateAsync(sendMessageTemplate, cancellationToken);

        return request.Id;
    }
}