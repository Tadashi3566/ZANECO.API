namespace ZANECO.API.Application.Identity.Users;

public class UserDetailsDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType? EmployeeId { get; set; }
    public string? SandurotId { get; set; }
    public string? AccountNumber { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? ImageUrl { get; set; }
}