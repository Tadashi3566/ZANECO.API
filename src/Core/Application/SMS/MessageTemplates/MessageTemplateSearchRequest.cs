using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateSearchRequest : PaginationFilter, IRequest<PaginationResponse<MessageTemplateDto>>
{
    public bool IncomingSchedule { get; set; }
}

public class MessageTemplateBySearchRequestSpec : EntitiesByPaginationFilterSpec<MessageTemplate, MessageTemplateDto>
{
    public MessageTemplateBySearchRequestSpec(MessageTemplateSearchRequest request)
        : base(request) => Query.Where(x => x.Schedule >= DateTime.Today, request.IncomingSchedule)
                                .OrderByDescending(c => c.Schedule, !request.HasOrderBy());
}

public class MessageTemplateSearchRequestHandler : IRequestHandler<MessageTemplateSearchRequest, PaginationResponse<MessageTemplateDto>>
{
    private readonly IReadRepository<MessageTemplate> _repository;

    public MessageTemplateSearchRequestHandler(IReadRepository<MessageTemplate> repository) => _repository = repository;

    public async Task<PaginationResponse<MessageTemplateDto>> Handle(MessageTemplateSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new MessageTemplateBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}