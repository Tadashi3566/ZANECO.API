using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using ZANECO.API.Application.Common.Persistence;
using ZANECO.API.Application.SMS;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Infrastructure.SMS;

internal class SmsService : ISmsService
{
    private readonly IDapperRepository _repositoryDapper;
    private readonly IRepositoryWithEvents<MessageLog> _repoMessageLog;
    private readonly IRepositoryWithEvents<MessageOut> _repoMessageOut;
    private readonly SmsSettings _smsSettings;

    public SmsService(IDapperRepository repositoryDapper, IRepositoryWithEvents<MessageLog> repoMessageLog, IRepositoryWithEvents<MessageOut> repoMessageOut, SmsSettings smsSettings) =>
        (_repositoryDapper, _repoMessageLog, _repoMessageOut, _smsSettings) = (repositoryDapper, repoMessageLog, repoMessageOut, smsSettings);

    public async Task SmsRead(int id)
    {
        _ = await _repositoryDapper.ExecuteScalarAsync<MessageIn>($"UPDATE datazaneco.MessageIn SET IsRead = 1, ReadOn = CURRENT_TIMESTAMP() WHERE Id LIKE '{id}'");
    }

    public async Task<string> SendToAPI(string phoneNumber, string message)
    {
        //if (phoneNumber.Length.Equals(0)) return string.Empty;

        if (phoneNumber.Trim().Length < 9) return string.Empty;

        phoneNumber = $"+639{phoneNumber[^9..]}";

        _smsSettings.msisdn = ClassSms.FormatContactNumber(phoneNumber);
        _smsSettings.content = message;

        string json = JsonConvert.SerializeObject(_smsSettings);

        using var client = new HttpClient();

        // This would be the like http://www.zaneco.ph
        client.BaseAddress = new Uri(_smsSettings.url);

        // serialize your json using newtonsoft json serializer then add it to the StringContent
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // method address would be like api/callUber:SomePort for example
        var result = await client.PostAsync(_smsSettings.url, content);

        string resultContent = await result.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<SMSResponse>(resultContent)!;

        return $"AccountNumber:{response!.code}\r\n" +
            $"Name:{response.name}\r\n" +
            $"TrxId:{response.transid}\r\n" +
            $"Timestamp:{response.timestamp}\r\n" +
            $"MsgCount:{response.msgcount}\r\n" +
            $"MsgId:{response.messageId}\r\n";
    }

    public async Task<int> SmsSend(string messageTo, string messageText, bool isCheckExisting = true, bool isAPI = true, string messageType = "sms.automatic")
    {
        byte[] sentenceBytes = Encoding.UTF8.GetBytes(messageText);
        byte[] hashBytes = SHA256.HashData(sentenceBytes);
        string messageHash = Convert.ToBase64String(hashBytes);

        if (isCheckExisting)
        {
            // Check existing SMS to send only once
            var existingMessage = await _repositoryDapper.QueryAsync<MessageLog>($"SELECT MessageTo, MessageHash FROM datazaneco.MessageLog WHERE MessageTo = '{messageTo}' AND MessageHash = '{messageHash}'");
            if (existingMessage.Count > 0) return 0;
        }

        if (isAPI)
        {
            string response = await SendToAPI(messageTo, messageText);
            string[] responseArray = response.Split("\r\n");
            int statusCode = Convert.ToInt16(responseArray[0][14..]);
            string statusText = responseArray[1][5..];
            string messageGuid = string.Empty;
            int messageParts = Convert.ToInt16(responseArray[4][9..]);
            string errorCode = string.Empty;
            string errorText = string.Empty;
            if (statusCode.Equals(201))
            {
                messageGuid = responseArray[2][6..];
                messageParts = Convert.ToInt16(responseArray[4][9..]);
            }
            else
            {
                errorCode = statusCode.ToString();
                errorText = statusText;
            }

            var messageLog = new MessageLog("ZANECO.API", "M360 API", "sms.automatic", "ZANECO", messageTo, messageText, messageGuid, messageParts, statusCode, statusText, errorCode, errorText, messageHash);
            _ = await _repoMessageLog.AddAsync(messageLog);
            return statusCode;
        }
        else
        {
            var messageOut = new MessageOut(messageType, messageTo, messageText);
            _ = await _repoMessageOut.AddAsync(messageOut);
            return 1;
        }
    }
}