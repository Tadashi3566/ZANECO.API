﻿namespace ZANECO.API.Infrastructure.SMS;

public class SmsSettings //: IValidatableObject
{
    public string app_key { get; set; } // = default!; // "ZvjB7jCBQPvUBYFp";
    public string app_secret { get; set; } // = default!; // "cKbyWdcnRDXDsPqzKF29fzLo0Bpj1HKd";
    public string shortcode_mask { get; set; } // = default!; // "ZANECO";
    public string url { get; set; } = default!; // "https://api.m360.com.ph/v3/api/broadcast";
    public string? msisdn { get; set; }
    public string? content { get; set; }
    public string cvd_transid { get; set; } = DefaultIdType.NewGuid().ToString();

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (string.IsNullOrEmpty(app_key))
    //        yield return new ValidationResult("No API Key defined in SMS settings config", new[] { nameof(app_key) });
    //    if (string.IsNullOrEmpty(app_secret))
    //        yield return new ValidationResult("No API Secret defined in SMS settings config", new[] { nameof(app_secret) });
    //    if (string.IsNullOrEmpty(shortcode_mask))
    //        yield return new ValidationResult("No API Mask defined in SMS settings config", new[] { nameof(shortcode_mask) });
    //    if (string.IsNullOrEmpty(url))
    //        yield return new ValidationResult("No API Url defined in SMS settings config", new[] { nameof(url) });
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