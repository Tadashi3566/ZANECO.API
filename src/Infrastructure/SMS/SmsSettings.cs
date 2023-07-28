namespace ZANECO.API.Infrastructure.SMS;

public class SmsSettings //: IValidatableObject
{
    public string app_key { get; set; } = "ZvjB7jCBQPvUBYFp";
    public string app_secret { get; set; } = "cKbyWdcnRDXDsPqzKF29fzLo0Bpj1HKd";
    public string shortcode_mask { get; set; } = "ZANECO";
    public string url { get; set; } = "https://api.m360.com.ph/v3/api/broadcast";
    public string? msisdn { get; set; }
    public string? content { get; set; }
    public string cvd_transid { get; set; } = DefaultIdType.NewGuid().ToString();

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (PermitLimit.Equals(default))
    //    {
    //        yield return new ValidationResult("No Permit Limit defined in Rate Limiter config", new[] { nameof(PermitLimit) });
    //    }
    //}
}

public class SMSResponse
{
    public string? code { get; set; }
    public string? name { get; set; }
    public string? transid { get; set; }
    public string? timestamp { get; set; }
    public int msgcount { get; set; }
    public int telco_id { get; set; }
    public string? messageId { get; set; }
}