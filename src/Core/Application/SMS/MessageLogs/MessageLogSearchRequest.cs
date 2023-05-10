using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogSearchRequest : PaginationFilter, IRequest<PaginationResponse<MessageLogDto>>
{
}

public class MessageLogBySearchRequestSpec : EntitiesByPaginationFilterSpec<MessageLog, MessageLogDto>
{
    public MessageLogBySearchRequestSpec(MessageLogSearchRequest request)
        : base(request) => Query.OrderByDescending(c => c.Id, !request.HasOrderBy());
}

public class MessageLogSearchRequestHandler : IRequestHandler<MessageLogSearchRequest, PaginationResponse<MessageLogDto>>
{
    private readonly IReadRepository<MessageLog> _repository;

    public MessageLogSearchRequestHandler(IReadRepository<MessageLog> repository) => _repository = repository;

    public async Task<PaginationResponse<MessageLogDto>> Handle(MessageLogSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new MessageLogBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}