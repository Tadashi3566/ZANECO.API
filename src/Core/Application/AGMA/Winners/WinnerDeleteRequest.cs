using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public WinnerDeleteRequest(Guid id) => Id = id;
}

public class WinnerDeleteRequestHandler : IRequestHandler<WinnerDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Winner> _repository;
    private readonly IStringLocalizer<WinnerDeleteRequestHandler> _localizer;

    public WinnerDeleteRequestHandler(IRepositoryWithEvents<Winner> repository, IStringLocalizer<WinnerDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(WinnerDeleteRequest request, CancellationToken cancellationToken)
    {
        var winner = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = winner ?? throw new NotFoundException($"Winner {request.Id} not found.");

        await _repository.DeleteAsync(winner, cancellationToken);

        return request.Id;
    }
}