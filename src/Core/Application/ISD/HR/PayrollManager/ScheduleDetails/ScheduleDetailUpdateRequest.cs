using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public Guid ScheduleId { get; set; }
    public string ScheduleType { get; set; } = default!; // DAYOFF, WORK
    public string Day { get; set; } = default!; // MONDAY, TUESDAY, WEDNESDAY ...
    public string TimeIn1 { get; set; } = default!;
    public string TimeOut1 { get; set; } = default!;
    public string TimeIn2 { get; set; } = default!;
    public string TimeOut2 { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class ScheduleDetailUpdateRequestValidator : CustomValidator<ScheduleDetailUpdateRequest>
{
    public ScheduleDetailUpdateRequestValidator(IReadRepository<Schedule> repoSchedule, IStringLocalizer<ScheduleDetailUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.ScheduleId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoSchedule.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["schedule not found."], id));

        RuleFor(p => p.ScheduleType)
            .NotEmpty();

        RuleFor(p => p.Day)
            .NotEmpty();
    }
}

public class ScheduleDetailUpdateRequestHandler : IRequestHandler<ScheduleDetailUpdateRequest, Guid>
{
    private readonly IReadRepository<Schedule> _repoSchedule;
    private readonly IRepositoryWithEvents<ScheduleDetail> _repoScheduleDetail;
    private readonly IStringLocalizer<ScheduleDetailUpdateRequestHandler> _localizer;

    public ScheduleDetailUpdateRequestHandler(IReadRepository<Schedule> repoSchedule, IRepositoryWithEvents<ScheduleDetail> repoScheduleDetail, IStringLocalizer<ScheduleDetailUpdateRequestHandler> localizer) =>
        (_repoSchedule, _repoScheduleDetail, _localizer) = (repoSchedule, repoScheduleDetail, localizer);

    public async Task<Guid> Handle(ScheduleDetailUpdateRequest request, CancellationToken cancellationToken)
    {
        var schedule = await _repoSchedule.GetByIdAsync(request.ScheduleId, cancellationToken);

        var scheduleDetail = await _repoScheduleDetail.GetByIdAsync(request.Id, cancellationToken);
        _ = scheduleDetail ?? throw new NotFoundException(string.Format(_localizer["ScheduleDetail not found."], request.Id));

        TimeSpan ts1 = Convert.ToDateTime(request.TimeOut1) - Convert.ToDateTime(request.TimeIn1);
        TimeSpan ts2 = Convert.ToDateTime(request.TimeOut2) - Convert.ToDateTime(request.TimeIn2);

        int totalHours = ts1.Hours + ts2.Hours;

        var updatedScheduleDetail = scheduleDetail.Update(schedule!.Name, request.ScheduleType, request.Day, request.TimeIn1, request.TimeOut1, request.TimeIn2, request.TimeOut2, totalHours, request.Description, request.Notes);

        await _repoScheduleDetail.UpdateAsync(updatedScheduleDetail, cancellationToken);

        return request.Id;
    }
}