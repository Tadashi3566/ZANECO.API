using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberByIdSpec : Specification<Member, MemberDto>, ISingleResultSpecification
{
    public MemberByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}