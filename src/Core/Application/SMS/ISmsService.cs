﻿namespace ZANECO.API.Application.SMS;

public interface ISmsService
{
    public Task SmsRead(int id);

    public Task<int> SmsSend(string messageTo, string messageText, bool isAPI = true, string messageType = "sms.automatic");
}