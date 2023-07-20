using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public DocumentDeleteRequest(Guid id) => Id = id;
}

public class DocumentDeleteRequestHandler : IRequestHandler<DocumentDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Document> _repository;
    private readonly IStringLocalizer<DocumentDeleteRequestHandler> _localizer;

    public DocumentDeleteRequestHandler(IRepositoryWithEvents<Document> repository, IStringLocalizer<DocumentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DocumentDeleteRequest request, CancellationToken cancellationToken)
    {
        var document = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = document ?? throw new NotFoundException($"Documents {request.Id} not found.");

        await _repository.DeleteAsync(document, cancellationToken);

        return request.Id;
    }
}