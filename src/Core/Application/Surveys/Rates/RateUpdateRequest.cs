using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class RateUpdateRequestValidator : CustomValidator<RateUpdateRequest>
{
    public RateUpdateRequestValidator(IReadRepository<Rate> RateRepo, IStringLocalizer<RateUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Number)
            .GreaterThan(0)
            .MustAsync(async (Rate, number, ct) =>
                    await RateRepo.FirstOrDefaultAsync(new RateByNumberSpec(number), ct)
                        is not { } existingRate || existingRate.Id == Rate.Id)
                .WithMessage((_, number) => string.Format(localizer["rate already exists"], number));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(16)
            .MustAsync(async (Rate, name, ct) =>
                    await RateRepo.FirstOrDefaultAsync(new RateByNameSpec(name), ct)
                        is not { } existingRate || existingRate.Id == Rate.Id)
                .WithMessage((_, name) => string.Format(localizer["rate already exists"], name));
    }
}

public class RateUpdateRequestHandler : IRequestHandler<RateUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Rate> _repository;
    private readonly IStringLocalizer<RateUpdateRequestHandler> _localizer;

    public RateUpdateRequestHandler(IRepositoryWithEvents<Rate> repository, IStringLocalizer<RateUpdateRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RateUpdateRequest request, CancellationToken cancellationToken)
    {
        var rate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = rate ?? throw new NotFoundException($"rate {request.Id} not found.");

        var updatedRate = rate.Update(request.Number, request.Name, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedRate, cancellationToken);

        return request.Id;
    }
}