using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleCreateRequest : BaseRequest, IRequest<Guid>
{
}

public class CreateScheduleRequestValidator : CustomValidator<ScheduleCreateRequest>
{
    public CreateScheduleRequestValidator(IReadRepository<Schedule> repoSchedule, IStringLocalizer<CreateScheduleRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (name, ct) => await repoSchedule.FirstOrDefaultAsync(new ScheduleByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["Schedule already exists."], name));
    }
}

public class ScheduleCreateRequestHandler : IRequestHandler<ScheduleCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Schedule> _repository;

    public ScheduleCreateRequestHandler(IRepositoryWithEvents<Schedule> repository) => _repository = repository;

    public async Task<Guid> Handle(ScheduleCreateRequest request, CancellationToken cancellationToken)
    {
        var schedule = new Schedule(request.Name, request.Description, request.Notes);

        await _repository.AddAsync(schedule, cancellationToken);

        return schedule.Id;
    }
}