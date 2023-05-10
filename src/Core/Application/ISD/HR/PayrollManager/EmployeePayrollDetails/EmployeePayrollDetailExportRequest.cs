using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? EmployeeId { get; set; }
}

public class EmployeePayrollDetailExportRequestHandler : IRequestHandler<EmployeePayrollDetailExportRequest, Stream>
{
    private readonly IReadRepository<EmployeePayrollDetail> _repository;
    private readonly IExcelWriter _excelWriter;

    public EmployeePayrollDetailExportRequestHandler(IReadRepository<EmployeePayrollDetail> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(EmployeePayrollDetailExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeePayrollDetailExportWithEmployeeSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class EmployeePayrollDetailExportWithEmployeeSpecification : EntitiesByBaseFilterSpec<EmployeePayrollDetail, EmployeePayrollDetailExportDto>
{
    public EmployeePayrollDetailExportWithEmployeeSpecification(EmployeePayrollDetailExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Employee)
            .Where(p => p.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}