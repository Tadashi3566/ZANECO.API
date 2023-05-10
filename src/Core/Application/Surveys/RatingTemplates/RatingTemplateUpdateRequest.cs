using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid RateId { get; set; }
    public string Comment { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public class RatingTemplateUpdateRequestValidator : CustomValidator<RatingTemplateUpdateRequest>
{
    public RatingTemplateUpdateRequestValidator(IReadRepository<Rate> rateRepo, IStringLocalizer<RatingTemplateUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.RateId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await rateRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["rate not found."], id));

        RuleFor(p => p.Comment)
            .NotEmpty()
            .MaximumLength(1024);
    }
}

public class RatingTemplateUpdateRequestHandler : IRequestHandler<RatingTemplateUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<RatingTemplate> _repository;
    private readonly IStringLocalizer<RatingTemplateUpdateRequestHandler> _localizer;

    public RatingTemplateUpdateRequestHandler(IRepositoryWithEvents<RatingTemplate> repository, IStringLocalizer<RatingTemplateUpdateRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RatingTemplateUpdateRequest request, CancellationToken cancellationToken)
    {
        var ratingTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ratingTemplate ?? throw new NotFoundException(string.Format(_localizer["RatingTemplate not found."], request.Id));

        var updatedRatingTemplate = ratingTemplate.Update(request.RateId, request.Comment, request.Description!, request.Notes!);

        await _repository.UpdateAsync(updatedRatingTemplate, cancellationToken);

        return request.Id;
    }
}