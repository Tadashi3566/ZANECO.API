using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateByNameSpec : Specification<MessageTemplate>, ISingleResultSpecification<MessageTemplate>
{
    public MessageTemplateByNameSpec(string message) =>
        Query.Where(p => p.Message == message);
}