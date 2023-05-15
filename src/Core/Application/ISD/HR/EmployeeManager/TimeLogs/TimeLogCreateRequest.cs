using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType EmployeeId { get; set; }
    public string LogType { get; set; } = default!;
    public DateTime LogDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public ImageUploadRequest? Image { get; set; }
}

public class CreateTimeLogRequestValidator : CustomValidator<TimeLogCreateRequest>
{
    public CreateTimeLogRequestValidator()
    {
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

        string imagePath = await _file.UploadAsync<TimeLog>(request.Image, FileType.Image, cancellationToken);

        var timeLog = new TimeLog(request.EmployeeId, employee!.FullName(), request.LogType, DateTime.Now, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(timeLog, cancellationToken);

        var attendance = await _repoAttendance.FirstOrDefaultAsync(new AttendanceByDateSpec(request.EmployeeId, request.LogDate!), cancellationToken);

        if (attendance is not null)
        {
            switch (request.LogType)
            {
                case "TIMEIN1":
                    var attendanceTimeIn1 = attendance.TimeIn1(DateTime.Now.AddMinutes(-3), imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn1, cancellationToken);
                    break;

                case "TIMEOUT1":
                    var attendanceTimeOut1 = attendance.TimeOut1(DateTime.Now, imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut1, cancellationToken);
                    break;

                case "TIMEIN2":
                    var attendanceTimeIn2 = attendance.TimeIn2(DateTime.Now.AddMinutes(-3), imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeIn2, cancellationToken);
                    break;

                case "TIMEOUT2":
                    var attendanceTimeOut2 = attendance.TimeOut2(DateTime.Now, imagePath);
                    await _repoAttendance.UpdateAsync(attendanceTimeOut2, cancellationToken);
                    break;
            }
        }

        return timeLog.Id;
    }
}