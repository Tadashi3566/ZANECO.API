using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogBySendTimeSpec : Specification<MessageLog, MessageLogDto>, ISingleResultSpecification<MessageLog>
{
    public MessageLogBySendTimeSpec(DateTime dtFrom, DateTime dtTo) =>
        Query.Where(p => p.SendTime >= dtFrom && p.SendTime <= dtTo);
}