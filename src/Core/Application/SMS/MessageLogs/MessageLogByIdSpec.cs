using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageLogs;

public class MessageLogByIdSpec : Specification<MessageLog, MessageLogDto>, ISingleResultSpecification<MessageLog>
{
    public MessageLogByIdSpec(int id) =>
        Query.Where(p => p.Id == id);
}