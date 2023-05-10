using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? EmployeeId { get; set; }
}

public class DependentExportRequestHandler : IRequestHandler<DependentExportRequest, Stream>
{
    private readonly IReadRepository<Dependent> _repository;
    private readonly IExcelWriter _excelWriter;

    public DependentExportRequestHandler(IReadRepository<Dependent> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(DependentExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new DependentExportWithEmployeeSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class DependentExportWithEmployeeSpecification : EntitiesByBaseFilterSpec<Dependent, DependentExportDto>
{
    public DependentExportWithEmployeeSpecification(DependentExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Employee)
            .Where(p => p.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}