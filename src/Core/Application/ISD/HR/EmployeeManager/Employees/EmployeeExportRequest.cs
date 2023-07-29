using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeExportRequest : BaseFilter, IRequest<Stream>
{
    public string Position { get; set; } = default!;
}

public class EmployeeExportRequestHandler : IRequestHandler<EmployeeExportRequest, Stream>
{
    private readonly IReadRepository<Employee> _repository;
    private readonly IExcelWriter _excelWriter;

    public EmployeeExportRequestHandler(IReadRepository<Employee> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(EmployeeExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeExportWithGroupsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class EmployeeExportWithGroupsSpecification : EntitiesByBaseFilterSpec<Employee, EmployeeExportDto>
{
    public EmployeeExportWithGroupsSpecification(EmployeeExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Position)
            .Where(p => p.Position.Equals(request.Position), request.Position.Length > 0);
}