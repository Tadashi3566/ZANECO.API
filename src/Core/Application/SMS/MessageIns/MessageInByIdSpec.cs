using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInByIdSpec : Specification<MessageIn, MessageInDto>, ISingleResultSpecification<MessageIn>
{
    public MessageInByIdSpec(int id) =>
        Query.Where(p => p.Id == id);
}