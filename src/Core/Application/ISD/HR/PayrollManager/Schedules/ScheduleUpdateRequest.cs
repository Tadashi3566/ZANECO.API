using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleUpdateRequest : RequestExtension ScheduleUpdateRequest>, IRequest<Guid>
{
}

public class ScheduleUpdateRequestValidator : CustomValidator<ScheduleUpdateRequest>
{
    public ScheduleUpdateRequestValidator(IReadRepository<Schedule> repoSchedule, IStringLocalizer<ScheduleUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (Schedule, name, ct) => await repoSchedule.FirstOrDefaultAsync(new ScheduleByNameSpec(name), ct)
                        is not { } existingSchedule || existingSchedule.Id == Schedule.Id)
            .WithMessage((_, name) => string.Format(localizer["Schedule already exists."], name));
    }
}

public class ScheduleUpdateRequestHandler : IRequestHandler<ScheduleUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Schedule> _repository;
    private readonly IStringLocalizer<ScheduleUpdateRequestHandler> _localizer;

    public ScheduleUpdateRequestHandler(IRepositoryWithEvents<Schedule> repository, IStringLocalizer<ScheduleUpdateRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(ScheduleUpdateRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = schedule ?? throw new NotFoundException($"Schedule {request.Id} not found.");

        var updatedSchedule = schedule.Update(request.Name, request.Description, request.Notes);

        await _repository.UpdateAsync(updatedSchedule, cancellationToken);

        return request.Id;
    }
}