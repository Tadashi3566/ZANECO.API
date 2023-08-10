using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateUpdateRequest : BaseRequest, IRequest<Guid>
{
    public Guid RateId { get; set; }
    public string Comment { get; set; } = default!;
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

    public RatingTemplateUpdateRequestHandler(IRepositoryWithEvents<RatingTemplate> repository) =>
        _repository = repository;

    public async Task<Guid> Handle(RatingTemplateUpdateRequest request, CancellationToken cancellationToken)
    {
        var ratingTemplate = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = ratingTemplate ?? throw new NotFoundException($"RatingTemplate {request.Id} not found.");

        var updatedRatingTemplate = ratingTemplate.Update(request.RateId, request.Comment, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedRatingTemplate, cancellationToken);

        return request.Id;
    }
}