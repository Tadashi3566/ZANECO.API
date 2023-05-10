using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerGetRequest : IRequest<WinnerDto>
{
    public DefaultIdType Id { get; set; }

    public WinnerGetRequest(Guid id) => Id = id;
}

public class WinnerGetRequestHandler : IRequestHandler<WinnerGetRequest, WinnerDto>
{
    private readonly IRepositoryWithEvents<Winner> _repository;
    private readonly IStringLocalizer<WinnerGetRequestHandler> _localizer;

    public WinnerGetRequestHandler(IRepositoryWithEvents<Winner> repository, IStringLocalizer<WinnerGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<WinnerDto> Handle(WinnerGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Winner, WinnerDto>)new WinnerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Winner not found."], request.Id));
}