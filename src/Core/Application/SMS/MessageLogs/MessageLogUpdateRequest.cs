using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogUpdateRequest : IRequest<int>
{
    public int Id { get; set; }
    public string MessageFrom { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class MessageLogUpdateRequestValidator : CustomValidator<MessageLogUpdateRequest>
{
}

public class MessageLogUpdateRequestHandler : IRequestHandler<MessageLogUpdateRequest, int>
{
    private readonly IRepositoryWithEvents<MessageLog> _repository;
    private readonly IStringLocalizer<MessageLogUpdateRequestHandler> _localizer;

    public MessageLogUpdateRequestHandler(IRepositoryWithEvents<MessageLog> repository, IStringLocalizer<MessageLogUpdateRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<int> Handle(MessageLogUpdateRequest request, CancellationToken cancellationToken)
    {
        var essageLog = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = essageLog ?? throw new NotFoundException(string.Format(_localizer["Message not found."], request.Id));

        var updatedMessageLog = essageLog.Update(request.Description, request.Notes);

        await _repository.UpdateAsync(updatedMessageLog, cancellationToken);

        return request.Id;
    }
}