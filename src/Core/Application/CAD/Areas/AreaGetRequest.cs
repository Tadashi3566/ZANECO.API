using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaGetRequest : IRequest<AreaDto>
{
    public DefaultIdType Id { get; set; }

    public AreaGetRequest(Guid id) => Id = id;
}

public class AreaGetRequestHandler : IRequestHandler<AreaGetRequest, AreaDto>
{
    private readonly IRepository<Area> _repository;
    private readonly IStringLocalizer<AreaGetRequestHandler> _localizer;

    public AreaGetRequestHandler(IRepository<Area> repository, IStringLocalizer<AreaGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AreaDto> Handle(AreaGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new AreaByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Area not found."], request.Id));
}