using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogByMessageSpec : Specification<MessageLog, MessageLogDto>, ISingleResultSpecification<MessageLog>
{
    public MessageLogByMessageSpec(string messageTo, string messageText) =>
        Query.Where(p => p.MessageTo == messageTo
                            && p.MessageText == messageText);
}