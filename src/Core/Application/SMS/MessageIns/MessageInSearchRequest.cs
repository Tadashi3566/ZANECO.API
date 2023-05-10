using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInSearchRequest : PaginationFilter, IRequest<PaginationResponse<MessageInDto>>
{
}

public class MessageInBySearchRequestSpec : EntitiesByPaginationFilterSpec<MessageIn, MessageInDto>
{
    public MessageInBySearchRequestSpec(MessageInSearchRequest request)
        : base(request) => Query.OrderByDescending(c => c.Id, !request.HasOrderBy());
}

public class MessageInSearchRequestHandler : IRequestHandler<MessageInSearchRequest, PaginationResponse<MessageInDto>>
{
    private readonly IReadRepository<MessageIn> _repository;

    public MessageInSearchRequestHandler(IReadRepository<MessageIn> repository) => _repository = repository;

    public async Task<PaginationResponse<MessageInDto>> Handle(MessageInSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new MessageInBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}