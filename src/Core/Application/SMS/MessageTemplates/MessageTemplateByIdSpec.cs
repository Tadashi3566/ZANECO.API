using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateByIdSpec : Specification<MessageTemplate, MessageTemplateDto>, ISingleResultSpecification
{
    public MessageTemplateByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}