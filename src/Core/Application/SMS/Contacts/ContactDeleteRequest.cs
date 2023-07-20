using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.Contacts;

public class DeleteContactRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteContactRequest(Guid id) => Id = id;
}

public class ContactDeleteRequestHandler : IRequestHandler<DeleteContactRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contact> _repository;
    private readonly IStringLocalizer<ContactDeleteRequestHandler> _localizer;

    public ContactDeleteRequestHandler(IRepositoryWithEvents<Contact> repository, IStringLocalizer<ContactDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = contact ?? throw new NotFoundException($"Contact {request.Id} not found.");

        await _repository.DeleteAsync(contact, cancellationToken);

        return request.Id;
    }
}