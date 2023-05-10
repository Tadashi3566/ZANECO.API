using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RatingTemplateDeleteRequest(Guid id) => Id = id;
}

public class RatingTemplateDeleteRequestHandler : IRequestHandler<RatingTemplateDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<RatingTemplate> _repository;
    private readonly IStringLocalizer<RatingTemplateDeleteRequestHandler> _localizer;

    public RatingTemplateDeleteRequestHandler(IRepositoryWithEvents<RatingTemplate> repository, IStringLocalizer<RatingTemplateDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RatingTemplateDeleteRequest request, CancellationToken cancellationToken)
    {
        var ratingTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = ratingTemplate ?? throw new NotFoundException(_localizer["ratingTemplate not found."]);

        await _repository.DeleteAsync(ratingTemplate, cancellationToken);

        return request.Id;
    }
}