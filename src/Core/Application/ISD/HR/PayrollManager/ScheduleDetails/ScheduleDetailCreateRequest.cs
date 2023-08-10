using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailCreateRequest : RequestExtension ScheduleDetailCreateRequest>, IRequest<Guid>
{
    public Guid ScheduleId { get; set; }
    public string ScheduleType { get; set; } = default!; // DAYOFF, WORK
    public string Day { get; set; } = default!; // MONDAY, TUESDAY, WEDNESDAY ...
    public string TimeIn1 { get; set; } = default!;
    public string TimeOut1 { get; set; } = default!;
    public string TimeIn2 { get; set; } = default!;
    public string TimeOut2 { get; set; } = default!;
}

public class CreateScheduleDetailRequestValidator : CustomValidator<ScheduleDetailCreateRequest>
{
    public CreateScheduleDetailRequestValidator(IReadRepository<Schedule> repoSchedule, IStringLocalizer<CreateScheduleDetailRequestValidator> localizer)
    {
        RuleFor(p => p.ScheduleId)
            .MustAsync(async (id, ct) => await repoSchedule.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["schedule not found."], id));

        RuleFor(p => p.ScheduleType)
            .NotEmpty();

        RuleFor(p => p.Day)
            .NotEmpty();
    }
}

public class ScheduleDetailCreateRequestHandler : IRequestHandler<ScheduleDetailCreateRequest, Guid>
{
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IRepositoryWithEvents<ScheduleDetail> _repoScheduleDetail;

    public ScheduleDetailCreateRequestHandler(IReadRepository<Schedule> repoSchedule, IRepositoryWithEvents<ScheduleDetail> repoScheduleDetail) =>
        (_repoSchedule, _repoScheduleDetail) = (repoSchedule, repoScheduleDetail);

    public async Task<Guid> Handle(ScheduleDetailCreateRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repoSchedule.GetByIdAsync(request.ScheduleId, cancellationToken);

        TimeSpan ts1 = Convert.ToDateTime(request.TimeOut1) - Convert.ToDateTime(request.TimeIn1);
        TimeSpan ts2 = Convert.ToDateTime(request.TimeOut2) - Convert.ToDateTime(request.TimeIn2);

        int totalHours = ts1.Hours + ts2.Hours;

        var scheduleDetail = new ScheduleDetail(request.ScheduleId, schedule!.Name, request.ScheduleType, request.Day, request.TimeIn1, request.TimeOut1, request.TimeIn2, request.TimeOut2, totalHours, request.Description, request.Notes);

        await _repoScheduleDetail.AddAsync(scheduleDetail, cancellationToken);

        return scheduleDetail.Id;
    }
}