using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageTemplates;

public class MessageTemplateByScheduleSpec : Specification<MessageTemplate>
{
    public MessageTemplateByScheduleSpec() =>
        Query.Where(p => p.Schedule >= DateTime.Today);
}