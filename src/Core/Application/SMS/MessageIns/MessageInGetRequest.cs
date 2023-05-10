using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInGetRequest : IRequest<MessageInDto>
{
    public int Id { get; set; }

    public MessageInGetRequest(int id) => Id = id;
}

public class MessageInGetRequestHandler : IRequestHandler<MessageInGetRequest, MessageInDto>
{
    private readonly IRepositoryWithEvents<MessageIn> _repository;
    private readonly IStringLocalizer<MessageInGetRequestHandler> _localizer;

    public MessageInGetRequestHandler(IRepositoryWithEvents<MessageIn> repository, IStringLocalizer<MessageInGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageInDto> Handle(MessageInGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<MessageIn, MessageInDto>)new MessageInByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Message not found."], request.Id));
}