using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public SalaryDeleteRequest(DefaultIdType id) => Id = id;
}

public class SalaryDeleteRequestHandler : IRequestHandler<SalaryDeleteRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Salary> _repository;
    private readonly IStringLocalizer<SalaryDeleteRequestHandler> _localizer;

    public SalaryDeleteRequestHandler(IRepositoryWithEvents<Salary> repository, IStringLocalizer<SalaryDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(SalaryDeleteRequest request, CancellationToken cancellationToken)
    {
        var salary = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = salary ?? throw new NotFoundException($"salary {request.Id} not found.");

        await _repository.DeleteAsync(salary, cancellationToken);

        return request.Id;
    }
}