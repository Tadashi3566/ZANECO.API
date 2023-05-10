using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? EmployeeId { get; set; }
}

public class EmployerExportRequestHandler : IRequestHandler<EmployerExportRequest, Stream>
{
    private readonly IReadRepository<Employer> _repository;
    private readonly IExcelWriter _excelWriter;

    public EmployerExportRequestHandler(IReadRepository<Employer> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(EmployerExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployerExportWithEmployeeSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class EmployerExportWithEmployeeSpecification : EntitiesByBaseFilterSpec<Employer, EmployerExportDto>
{
    public EmployerExportWithEmployeeSpecification(EmployerExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Employee)
            .Where(p => p.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}