using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactGetRequest : IRequest<ContactDto>
{
    public Guid Id { get; set; }

    public ContactGetRequest(Guid id) => Id = id;
}

public class ContactGetRequestHandler : IRequestHandler<ContactGetRequest, ContactDto>
{
    private readonly IRepositoryWithEvents<Contact> _repository;
    private readonly IStringLocalizer<ContactGetRequestHandler> _localizer;

    public ContactGetRequestHandler(IRepositoryWithEvents<Contact> repository, IStringLocalizer<ContactGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ContactDto> Handle(ContactGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new ContactByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Contact not found."], request.Id));
}