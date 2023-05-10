using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutSearchRequest : PaginationFilter, IRequest<PaginationResponse<MessageOutDto>>
{
}

public class MessageOutBySearchRequestSpec : EntitiesByPaginationFilterSpec<MessageOut, MessageOutDto>
{
    public MessageOutBySearchRequestSpec(MessageOutSearchRequest request)
        : base(request) => Query.OrderBy(q => q.Id, !request.HasOrderBy());
}

public class MessageOutSearchRequestHandler : IRequestHandler<MessageOutSearchRequest, PaginationResponse<MessageOutDto>>
{
    private readonly IReadRepository<MessageOut> _repository;

    public MessageOutSearchRequestHandler(IReadRepository<MessageOut> repository) => _repository = repository;

    public async Task<PaginationResponse<MessageOutDto>> Handle(MessageOutSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new MessageOutBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}