using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentUpdateRequest : IRequest<int>
{
    public int Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string AppointmentType { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public DateTime StartDateTime { get; set; } = default!;
    public DateTime EndDateTime { get; set; } = default!;
    public string? Location { get; set; }
    public int Hours { get; set; } = default!;
    public bool IsAllDay { get; set; } = default!;

    //public bool IsReadonly { get; set; } = default!;
    //public bool IsBlock { get; set; } = default!;
    //public int? CalendarId { get; set; }
    //public int? RecurrenceID { get; set; }
    //public string? RecurrenceRule { get; set; }
    //public string? RecurrenceException { get; set; }
    //public string? CssClass { get; set; }

    public DefaultIdType RecommendedBy { get; set; }
    public DateTime? RecommendedOn { get; set; }
    public DefaultIdType ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class AppointmentUpdateRequestValidator : CustomValidator<AppointmentUpdateRequest>
{
    public AppointmentUpdateRequestValidator()
    {
        RuleFor(p => p.AppointmentType)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Subject)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class AppointmentUpdateRequestHandler : IRequestHandler<AppointmentUpdateRequest, int>
{
    private readonly IRepositoryWithEvents<Appointment> _repoAppointment;
    private readonly IStringLocalizer<AppointmentUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public AppointmentUpdateRequestHandler(IRepositoryWithEvents<Appointment> repoAppointment, IStringLocalizer<AppointmentUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoAppointment, _localizer, _file) = (repoAppointment, localizer, file);

    public async Task<int> Handle(AppointmentUpdateRequest request, CancellationToken cancellationToken)
    {
        var appointment = await _repoAppointment.GetByIdAsync(request.Id, cancellationToken);
        _ = appointment ?? throw new NotFoundException(string.Format(_localizer["Appointment not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentimagePath = appointment.ImagePath;
            if (!string.IsNullOrEmpty(currentimagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentimagePath));
            }

            appointment = appointment.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Appointment>(request.Image, FileType.Image, cancellationToken)
            : null;

        if (request.IsAllDay)
        {
            request.StartDateTime = new DateTime(request.StartDateTime.Year, request.StartDateTime.Month, request.StartDateTime.Day);
            request.EndDateTime = new DateTime(request.EndDateTime.Year, request.EndDateTime.Month, request.EndDateTime.Day, 23, 59, 59);
        }

        var updatedAppointment = appointment.Update(request.AppointmentType, request.Subject, request.StartDateTime, request.EndDateTime, request.Location, request.Hours, request.IsAllDay, request.RecommendedBy, request.ApprovedBy, request.Description, request.Notes, imagePath!);

        await _repoAppointment.UpdateAsync(updatedAppointment, cancellationToken);

        return request.Id;
    }
}