using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

public class DocumentGetViaDapperRequest : IRequest<DocumentDto>
{
    public DefaultIdType Id { get; set; }

    public DocumentGetViaDapperRequest(Guid id) => Id = id;
}

public class DocumentGetViaDapperRequestHandler : IRequestHandler<DocumentGetViaDapperRequest, DocumentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<DocumentGetViaDapperRequestHandler> _localizer;

    public DocumentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<DocumentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DocumentDto> Handle(DocumentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var document = await _repository.QueryFirstOrDefaultAsync<Document>(
            $"SELECT * FROM datazaneco.\"Documents\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = document ?? throw new NotFoundException($"document {request.Id} not found.");

        return document.Adapt<DocumentDto>();
    }
}