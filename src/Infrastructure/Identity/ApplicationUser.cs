using Microsoft.AspNetCore.Identity;

namespace ZANECO.API.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public Guid? EmployeeId { get; set; }
    public string? SandurotId { get; set; }
    public string? AccountNumber { get; set; }
    public string? ObjectId { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}