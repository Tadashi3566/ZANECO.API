using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentSearchRequest : PaginationFilter, IRequest<PaginationResponse<DocumentDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class DocumentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Document, DocumentDto>
{
    public DocumentsBySearchRequestSpec(DocumentSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.DocumentDate, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class DocumentSearchRequestHandler : IRequestHandler<DocumentSearchRequest, PaginationResponse<DocumentDto>>
{
    private readonly IReadRepository<Document> _repository;

    public DocumentSearchRequestHandler(IReadRepository<Document> repository) => _repository = repository;

    public async Task<PaginationResponse<DocumentDto>> Handle(DocumentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new DocumentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}