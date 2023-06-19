using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType EmployeeId { get; set; }
    public string Device { get; set; } = default!;
    public string LogType { get; set; } = default!;
    public DateTime LogDate { get; set; } = default!;
    public DateTime LogDateTime { get; set; } = default!;
    public int SyncId { get; set; } = default!;
    public string? Coordinates { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class CreateTimeLogRequestValidator : CustomValidator<TimeLogCreateRequest>
{
    public CreateTimeLogRequestValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();

        RuleFor(r => r.Device)
            .NotEmpty();

        RuleFor(r => r.LogType)
            .NotEmpty();

        RuleFor(r => r.LogDate)
            .NotNull();

        RuleFor(r => r.LogDateTime)
            .NotNull();

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class TimeLogCreateRequestHandler : IRequestHandler<TimeLogCreateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<TimeLog> _repository;
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IFileStorageService _file;

    public TimeLogCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<TimeLog> repository, IRepositoryWithEvents<Attendance> repoAttendance, IFileStorageService file) =>
        (_repoEmployee, _repository, _repoAttendance, _file) = (repoEmployee, repository, repoAttendance, file);

    public async Task<DefaultIdType> Handle(TimeLogCreateRequest request, CancellationToken cancellationToken)
    {
        // Get Employee Information
        var employee = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = employee ?? throw new NotFoundException("Employee not found.");
        if (!employee.IsActive) throw new Exception("Employee is no longer Active");

        var existingTimeLog = await _repository.FirstOrDefaultAsync(new TimeLogByFileNameSpec(request.Image.Name), cancellationToken);
        if (existingTimeLog is not null)
            return existingTimeLog.Id;

        string imagePath = await _file.UploadAsync<TimeLog>(request.Image, FileType.Image, cancellationToken);

        var timeLog = new TimeLog(request.EmployeeId, employee!.FullName(), request.Device, request.LogType, request.LogDate, request.LogDateTime, request.SyncId, request.Coordinates, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(timeLog, cancellationToken);

        var attendance = await _repoAttendance.FirstOrDefaultAsync(new AttendanceByDateSpec(request.EmployeeId, request.LogDate!), cancellationToken);

        if (attendance is not null)
        {
            switch (request.LogType)
            {
                case "TIMEIN":
                case "TIMEIN1":
                    var attendanceTimeIn1 = attendance.TimeIn1(request.LogDateTime.AddMinutes(-1), imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn1, cancellationToken);
                    break;

                case "TIMEOUT1":
                    var attendanceTimeOut1 = attendance.TimeOut1(request.LogDateTime, imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut1, cancellationToken);
                    break;

                case "TIMEIN2":
                    var attendanceTimeIn2 = attendance.TimeIn2(request.LogDateTime, imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn2, cancellationToken);
                    break;

                case "TIMEOUT":
                case "TIMEOUT2":
                    var attendanceTimeOut2 = attendance.TimeOut2(request.LogDateTime, imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut2, cancellationToken);
                    break;
            }
        }

        return timeLog.Id;
    }
}