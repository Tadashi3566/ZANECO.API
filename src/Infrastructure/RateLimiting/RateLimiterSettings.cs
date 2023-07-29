using System.ComponentModel.DataAnnotations;

namespace ZANECO.API.Infrastructure.RateLimiting;

public class RateLimiterSettings //: IValidatableObject
{
    public bool Enable { get; set; } = default!;
    public bool AutoReplenishment { get; set; } = default!;
    public int PermitLimit { get; set; } = default!;
    public int QueueLimit { get; set; } = default!;
    public int WindowInSecond { get; set; } = default!;
    public int SegmentsPerWindow { get; set; } = default!;
    public int TokenLimit { get; set; } = default!;
    public int ReplenishmentPeriodInSecond { get; set; } = default!;
    public int TokensPerPeriod { get; set; } = default!;

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (PermitLimit.Equals(default))
    //        yield return new ValidationResult("No Permit Limit defined in Rate Limiter config", new[] { nameof(PermitLimit) });

    //    if (QueueLimit.Equals(default))
    //        yield return new ValidationResult("No Queue Limit defined in Rate Limiter config", new[] { nameof(QueueLimit) });
    //}
}