using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutGetRequest : IRequest<MessageOutDto>
{
    public int Id { get; set; }

    public MessageOutGetRequest(int id) => Id = id;
}

public class MessageOutGetRequestHandler : IRequestHandler<MessageOutGetRequest, MessageOutDto>
{
    private readonly IRepositoryWithEvents<MessageOut> _repository;
    private readonly IStringLocalizer<MessageOutGetRequestHandler> _localizer;

    public MessageOutGetRequestHandler(IRepositoryWithEvents<MessageOut> repository, IStringLocalizer<MessageOutGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<MessageOutDto> Handle(MessageOutGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<MessageOut, MessageOutDto>)new MessageOutByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Message not found."], request.Id));
}