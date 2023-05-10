using Mapster;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateGetViaDapperRequest : IRequest<RatingTemplateDto>
{
    public DefaultIdType Id { get; set; }

    public RatingTemplateGetViaDapperRequest(Guid id) => Id = id;
}

public class RatingTemplateGetViaDapperRequestHandler : IRequestHandler<RatingTemplateGetViaDapperRequest, RatingTemplateDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RatingTemplateGetViaDapperRequestHandler> _localizer;

    public RatingTemplateGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<RatingTemplateGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RatingTemplateDto> Handle(RatingTemplateGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var ratingTemplate = await _repository.QueryFirstOrDefaultAsync<RatingTemplate>(
            $"SELECT * FROM datazaneco.\"RatingTemplates\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = ratingTemplate ?? throw new NotFoundException(string.Format(_localizer["ratingTemplate not found."], request.Id));

        return ratingTemplate.Adapt<RatingTemplateDto>();
    }
}