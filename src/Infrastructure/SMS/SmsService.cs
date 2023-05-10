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

    public SmsService(IDapperRepository repositoryDapper, IRepositoryWithEvents<MessageLog> repoMessageLog, IRepositoryWithEvents<MessageOut> repoMessageOut) =>
        (_repositoryDapper, _repoMessageLog, _repoMessageOut) = (repositoryDapper, repoMessageLog, repoMessageOut);

    public async Task SmsRead(int id)
    {
        _ = await _repositoryDapper.ExecuteScalarAsync<MessageIn>($"UPDATE datazaneco.MessageIn SET IsRead = 1, ReadOn = CURRENT_TIMESTAMP() WHERE Id LIKE '{id}'");
    }

    public async Task<int> SmsSend(string messageTo, string messageText, bool isAPI = false, string messageType = "sms.automatic")
    {
        byte[] sentenceBytes = Encoding.UTF8.GetBytes(messageText);
        string messageHash;

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] hashBytes = sha256Hash.ComputeHash(sentenceBytes);
            messageHash = Convert.ToBase64String(hashBytes);
        }

        // Check existing SMS to send only once
        var existingMessage = await _repositoryDapper.QueryAsync<MessageLog>($"SELECT MessageTo, MessageHash FROM datazaneco.MessageLog WHERE MessageTo = '{messageTo}' AND MessageHash = '{messageHash}'");
        if (existingMessage.Count > 0) return 0;

        if (isAPI)
        {
            ClassSms sms = new();
            string response = await sms.SendToAPI(messageTo, messageText);
            string[] responseArray = response.Split("\r\n");
            DateTime receiveDateTime = DateTime.Now;
            int statusCode = Convert.ToInt16(responseArray[0][14..]);
            string statusText = responseArray[1][5..];
            string messageGuid = string.Empty;
            int messageParts = Convert.ToInt16(responseArray[4][9..]);
            string errorCode = string.Empty;
            string errorText = string.Empty;
            if (statusCode.Equals("201"))
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