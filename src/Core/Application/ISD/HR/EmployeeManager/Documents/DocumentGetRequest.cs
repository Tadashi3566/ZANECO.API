using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentGetRequest : IRequest<DocumentDto>
{
    public DefaultIdType Id { get; set; }

    public DocumentGetRequest(Guid id) => Id = id;
}

public class DocumentGetRequestHandler : IRequestHandler<DocumentGetRequest, DocumentDto>
{
    private readonly IRepository<Document> _repository;
    private readonly IStringLocalizer<DocumentGetRequestHandler> _localizer;

    public DocumentGetRequestHandler(IRepository<Document> repository, IStringLocalizer<DocumentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DocumentDto> Handle(DocumentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Document, DocumentDto>)new DocumentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Documents not found."], request.Id));
}