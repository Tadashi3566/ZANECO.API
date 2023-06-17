using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateInterruptionRequest : PaginationFilter, IRequest<List<MessageTemplateDto>>
{

}

public class MessageTemplateByInterruptionRequestSpec : EntitiesByBaseFilterSpec<MessageTemplate, MessageTemplateDto>
{
    public MessageTemplateByInterruptionRequestSpec(MessageTemplateInterruptionRequest request)
        : base(request) => Query.Where(x => x.Schedule >= DateTime.Today)
                                .OrderBy(c => c.Schedule);
}

public class MessageTemplateInterruptionRequestHandler : IRequestHandler<MessageTemplateInterruptionRequest, List<MessageTemplateDto>>
{
    private readonly IReadRepository<MessageTemplate> _repository;

    public MessageTemplateInterruptionRequestHandler(IReadRepository<MessageTemplate> repository) => _repository = repository;

    public async Task<List<MessageTemplateDto>> Handle(MessageTemplateInterruptionRequest request, CancellationToken cancellationToken)
    {
        var spec = new MessageTemplateByInterruptionRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}