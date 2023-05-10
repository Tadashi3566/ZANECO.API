using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanSearchRequest : PaginationFilter, IRequest<PaginationResponse<LoanDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class LoansBySearchRequestSpec : EntitiesByPaginationFilterSpec<Loan, LoanDto>
{
    public LoansBySearchRequestSpec(LoanSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.EndDate, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class LoanSearchRequestHandler : IRequestHandler<LoanSearchRequest, PaginationResponse<LoanDto>>
{
    private readonly IReadRepository<Loan> _repository;

    public LoanSearchRequestHandler(IReadRepository<Loan> repository) => _repository = repository;

    public async Task<PaginationResponse<LoanDto>> Handle(LoanSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new LoansBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}