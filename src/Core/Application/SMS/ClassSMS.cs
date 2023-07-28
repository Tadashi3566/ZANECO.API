using Newtonsoft.Json;
using System.Text;

namespace ZANECO.API.Application.SMS;

//internal class SMS
//{
//    public string app_key { get; set; } = "ZvjB7jCBQPvUBYFp";
//    public string app_secret { get; set; } = "cKbyWdcnRDXDsPqzKF29fzLo0Bpj1HKd";
//    public string? msisdn { get; set; }
//    public string? content { get; set; }
//    public string shortcode_mask { get; set; } = "ZANECO";
//    public string cvd_transid { get; set; } = DefaultIdType.NewGuid().ToString();
//}

//internal class SMSResponse
//{
//    public string? code { get; set; }
//    public string? name { get; set; }
//    public string? transid { get; set; }
//    public string? timestamp { get; set; }
//    public int msgcount { get; set; }
//    public int telco_id { get; set; }
//    public string? messageId { get; set; }
//}

public static class ClassSms
{
    //public readonly string URL = "https://api.m360.com.ph/v3/api/broadcast";

    public static string FormatContactNumber(string contactNumber)
    {
        if (contactNumber.Length <= 9) return contactNumber;

        contactNumber = contactNumber.Trim();

        return $"+639{contactNumber[^9..]}";
    }

    public static string RemoveWhiteSpaces(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());
    }

    public static string[] GetDistinctFromArray(string recipients)
    {
        recipients = RemoveWhiteSpaces(recipients);
        string[] recipientArray = recipients.Split(',');

        return recipientArray.Distinct().ToArray();
    }

    //public async Task<string> SendToAPI(string phoneNumber, string message)
    //{
    //    if (phoneNumber.Length.Equals(0)) return string.Empty;

    //    if (phoneNumber.Trim().Length < 9)
    //    {
    //        return string.Empty;
    //    }

    //    phoneNumber = $"+639{phoneNumber[^9..]}";

    //    SMS sms = new()
    //    {
    //        msisdn = FormatContactNumber(phoneNumber),
    //        content = message,
    //    };

    //    string json = JsonConvert.SerializeObject(sms);

    //    using var client = new HttpClient();

    //    // This would be the like http://www.zaneco.ph
    //    client.BaseAddress = new Uri(URL);

    //    // serialize your json using newtonsoft json serializer then add it to the StringContent
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");

    //    // method address would be like api/callUber:SomePort for example
    //    var result = await client.PostAsync(URL, content);

    //    string resultContent = await result.Content.ReadAsStringAsync();

    //    var response = JsonConvert.DeserializeObject<SMSResponse>(resultContent)!;

    //    return $"AccountNumber:{response!.code}\r\n" +
    //        $"Name:{response.name}\r\n" +
    //        $"TrxId:{response.transid}\r\n" +
    //        $"Timestamp:{response.timestamp}\r\n" +
    //        $"MsgCount:{response.msgcount}\r\n" +
    //        $"MsgId:{response.messageId}\r\n";
    //}
}