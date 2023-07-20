using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public ContributionDeleteRequest(Guid id) => Id = id;
}

public class ContributionDeleteRequestHandler : IRequestHandler<ContributionDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Contribution> _repository;
    private readonly IStringLocalizer<ContributionDeleteRequestHandler> _localizer;

    public ContributionDeleteRequestHandler(IRepositoryWithEvents<Contribution> repository, IStringLocalizer<ContributionDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ContributionDeleteRequest request, CancellationToken cancellationToken)
    {
        var contribution = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = contribution ?? throw new NotFoundException($"Contribution {request.Id} not found.");

        await _repository.DeleteAsync(contribution, cancellationToken);

        return request.Id;
    }
}