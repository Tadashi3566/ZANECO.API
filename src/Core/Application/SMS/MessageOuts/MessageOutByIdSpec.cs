﻿using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutByIdSpec : Specification<MessageOut, MessageOutDto>, ISingleResultSpecification<MessageOut>
{
    public MessageOutByIdSpec(int id) =>
        Query.Where(p => p.Id == id);
}