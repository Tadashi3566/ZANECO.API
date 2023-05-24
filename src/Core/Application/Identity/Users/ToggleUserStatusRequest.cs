namespace ZANECO.API.Application.Identity.Users;

public class ToggleUserStatusRequest
{
    public DefaultIdType? EmployeeId { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool ActivateUser { get; set; }
    public string? SandurotId { get; set; }
    public string? AccountNumber { get; set; }
    public string? UserId { get; set; }
}