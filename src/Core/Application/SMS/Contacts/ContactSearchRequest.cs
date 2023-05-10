using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactSearchRequest : PaginationFilter, IRequest<PaginationResponse<ContactDto>>
{
}

public class ContactBySearchRequestSpec : EntitiesByPaginationFilterSpec<Contact, ContactDto>
{
    public ContactBySearchRequestSpec(ContactSearchRequest request)
        : base(request) => Query.OrderBy(q => q.Name, !request.HasOrderBy());
}

public class ContactSearchRequestHandler : IRequestHandler<ContactSearchRequest, PaginationResponse<ContactDto>>
{
    private readonly IReadRepository<Contact> _repository;

    public ContactSearchRequestHandler(IReadRepository<Contact> repository) => _repository = repository;

    public async Task<PaginationResponse<ContactDto>> Handle(ContactSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new ContactBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}