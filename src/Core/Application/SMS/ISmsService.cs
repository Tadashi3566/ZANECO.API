namespace ZANECO.API.Application.SMS;

public interface ISmsService
{
    public Task SmsRead(int id);

    public Task<string> SendToAPI(string phoneNumber, string message);

    public Task<int> SmsSend(string messageTo, string messageText, bool isCheckExisting = true, bool isAPI = true, string messageType = "sms.automatic");
}