using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateCreateRequest : RequestExtension RateCreateRequest>, IRequest<Guid>
{
    public int Number { get; set; } = default!;
}

public class CreateRateRequestValidator : CustomValidator<RateCreateRequest>
{
    public CreateRateRequestValidator(IReadRepository<Rate> repoRate, IStringLocalizer<CreateRateRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (number, ct) => await repoRate.FirstOrDefaultAsync(new RateByNumberSpec(number), ct) is null)
            .WithMessage((_, number) => string.Format(localizer["rate already exists."], number));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (name, ct) => await repoRate.FirstOrDefaultAsync(new RateByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["rate already exists."], name));
    }
}

public class RateCreateRequestHandler : IRequestHandler<RateCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Rate> _repository;

    public RateCreateRequestHandler(IRepositoryWithEvents<Rate> repository) => _repository = repository;

    public async Task<Guid> Handle(RateCreateRequest request, CancellationToken cancellationToken)
    {
        var rate = new Rate(request.Number, request.Name, request.Description, request.Notes);

        await _repository.AddAsync(rate, cancellationToken);

        return rate.Id;
    }
}