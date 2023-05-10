using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Application.ISD.Members;

public class MemberByNameSpec : Specification<Member>, ISingleResultSpecification
{
    public MemberByNameSpec(string name) => Query.Where(p => p.Name == name);
}