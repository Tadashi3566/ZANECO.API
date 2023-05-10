using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string LogType { get; set; } = default!;
    public DateTime LogDate { get; set; }
    public DateTime LogDateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class TimeLogUpdateRequestValidator : CustomValidator<TimeLogUpdateRequest>
{
    public TimeLogUpdateRequestValidator(IReadRepository<TimeLog> TimeLogRepo, IStringLocalizer<TimeLogUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class TimeLogUpdateRequestHandler : IRequestHandler<TimeLogUpdateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IRepositoryWithEvents<TimeLog> _repoLog;
    private readonly IStringLocalizer<TimeLogUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public TimeLogUpdateRequestHandler(IRepositoryWithEvents<Attendance> repoAttendadnce, IRepositoryWithEvents<TimeLog> repoLog, IStringLocalizer<TimeLogUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoAttendance, _repoLog, _localizer, _file) = (repoAttendadnce, repoLog, localizer, file);

    public async Task<DefaultIdType> Handle(TimeLogUpdateRequest request, CancellationToken cancellationToken)
    {
        var timeLog = await _repoLog.GetByIdAsync(request.Id, cancellationToken);
        _ = timeLog ?? throw new NotFoundException(string.Format(_localizer["timeLog not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = timeLog.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            timeLog = timeLog.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<TimeLog>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedTimeLog = timeLog.Update(request.LogType, request.LogDate!, request.LogDateTime!, request.Description, request.Notes, imagePath!);

        await _repoLog.UpdateAsync(updatedTimeLog, cancellationToken);

        var attendance = await _repoAttendance.FirstOrDefaultAsync(new AttendanceByDateSpec(request.EmployeeId, request.LogDate!), cancellationToken);
        if (attendance is not null)
        {
            switch (request.LogType)
            {
                case "TIMEIN1":
                    var attendanceTimeIn1 = attendance.TimeIn1(request.LogDateTime, timeLog.ImagePath!);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn1, cancellationToken);
                    break;

                case "TIMEOUT1":
                    var attendanceTimeOut1 = attendance.TimeOut1(request.LogDateTime, timeLog.ImagePath!);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut1, cancellationToken);
                    break;

                case "TIMEIN2":
                    var attendanceTimeIn2 = attendance.TimeIn2(request.LogDateTime, timeLog.ImagePath!);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn2, cancellationToken);
                    break;

                case "TIMEOUT2":
                    var attendanceTimeOut2 = attendance.TimeOut2(request.LogDateTime, timeLog.ImagePath!);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut2, cancellationToken);
                    break;
            }
        }

        return request.Id;
    }
}