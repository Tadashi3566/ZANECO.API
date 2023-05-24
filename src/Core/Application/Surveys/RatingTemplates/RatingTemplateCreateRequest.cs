using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateCreateRequest : IRequest<Guid>
{
    public Guid RateId { get; set; }
    public string Comment { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class RatingTemplateCreateRequestValidator : CustomValidator<RatingTemplateCreateRequest>
{
    public RatingTemplateCreateRequestValidator(IReadRepository<Rate> RateRepo, IStringLocalizer<RatingTemplateCreateRequestValidator> localizer)
    {
        RuleFor(p => p.RateId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await RateRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["Rate not found."], id));

        RuleFor(p => p.Comment)
            .NotEmpty()
            .MaximumLength(1024);
    }
}

public class RatingTemplateCreateRequestHandler : IRequestHandler<RatingTemplateCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<RatingTemplate> _repository;

    public RatingTemplateCreateRequestHandler(IRepositoryWithEvents<RatingTemplate> repository) => _repository = repository;

    public async Task<Guid> Handle(RatingTemplateCreateRequest request, CancellationToken cancellationToken)
    {
        var ratingTemplate = new RatingTemplate(request.RateId, request.Comment, request.Description, request.Notes);

        await _repository.AddAsync(ratingTemplate, cancellationToken);

        return ratingTemplate.Id;
    }
}