using Mapster;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactGetViaDapperRequest : IRequest<ContactDto>
{
    public Guid Id { get; set; }

    public ContactGetViaDapperRequest(Guid id) => Id = id;
}

public class ContactViaDapperGetRequestHandler : IRequestHandler<ContactGetViaDapperRequest, ContactDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<ContactViaDapperGetRequestHandler> _localizer;

    public ContactViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<ContactViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ContactDto> Handle(ContactGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var contact = await _repository.QueryFirstOrDefaultAsync<Contact>(
        $"SELECT * FROM datazaneco.\"Contacts\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = contact ?? throw new NotFoundException($"Contact {request.Id} not found.");

        return contact.Adapt<ContactDto>();
    }
}