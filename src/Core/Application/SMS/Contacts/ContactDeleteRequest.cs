using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class ContactDeleteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public ContactDeleteRequest(Guid id) => Id = id;
}

public class ContactDeleteRequestHandler : IRequestHandler<ContactDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repository;
    private readonly IStringLocalizer<ContactDeleteRequestHandler> _localizer;

    public ContactDeleteRequestHandler(IRepositoryWithEvents<Contact> repository, IStringLocalizer<ContactDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ContactDeleteRequest request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = contact ?? throw new NotFoundException($"Contact {request.Id} not found.");

        await _repository.DeleteAsync(contact, cancellationToken);

        return request.Id;
    }
}