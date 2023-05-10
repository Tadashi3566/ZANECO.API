using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public AreaDeleteRequest(Guid id) => Id = id;
}

public class AreaDeleteRequestHandler : IRequestHandler<AreaDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Area> _repository;
    private readonly IStringLocalizer<AreaDeleteRequestHandler> _localizer;

    public AreaDeleteRequestHandler(IRepositoryWithEvents<Area> repository, IStringLocalizer<AreaDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(AreaDeleteRequest request, CancellationToken cancellationToken)
    {
        var area = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = area ?? throw new NotFoundException(_localizer["Area not found."]);

        await _repository.DeleteAsync(area, cancellationToken);

        return request.Id;
    }
}