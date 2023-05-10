namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInReadRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string MessageFrom { get; set; } = default!;
    public bool IsAgma { get; set; }
}

//public class MessageInReadRequestHandler : IRequestHandler<MessageInReadRequest, bool>
//{
//    private readonly IJobService _jobService;
//    private readonly ISmsService _smsService;
//    private readonly IDapperRepository _repository;

//    public MessageInReadRequestHandler(
//        IJobService jobService,
//        ISmsService smsService,
//        IDapperRepository repository) =>
//        (_jobService, _smsService, _repository) = (jobService, smsService, repository);

//    public async Task<bool> Handle(MessageInReadRequest request, CancellationToken cancellationToken)
//    {
//        return false;

//        if (request.IsAgma)
//        {
//            //Get all unread messages
//            var unreadMessages = await _repoRating.QueryListAsync<MessageIn>("SELECT Id, MessageFrom, MessageText, IsRead FROM datazaneco.MessageIn WHERE IsRead NOT LIKE 1", cancellationToken: cancellationToken);

//            //Check if message is AGMA Registrtion
//            if (unreadMessages is not null)
//            {
//                foreach (var message in unreadMessages)
//                {
//                    if (message.MessageText.Equals(string.Empty))
//                    {
//                        _jobService.Enqueue(() => _smsService.SmsRead(message.Id));

//                        continue;
//                    }

//                    string messageRaw = Regex.Replace(message.MessageText, "[^a-zA-Z0-9]", " ");
//                    string[] messageArray = messageRaw.Split(' ');

//                    //check every word as account number
//                    if (messageArray.Length.Equals(0))
//                        return false;

//                    foreach (string word in messageArray)
//                    {
//                        string accountNumber = word.Trim().ToUpper();

//                        if (accountNumber.Length >= 7 && accountNumber.Length <= 10 && accountNumber[..7].All(char.IsNumber))
//                        {
//                            _jobService.Enqueue(() => _smsService.SmsSend(message.MessageFrom, "Registration for our 39th ZANECO Virtual AGMA has been closed. We will be sending links for the Zoom, Facebook, and Youtube Live events on December 17, 2022", false, false, "sms.automatic"));
//                            _jobService.Enqueue(() => _smsService.SmsRead(message.Id));
//                            continue;

//                            //Check if Reference exist
//                            var account = await _repoRating.QueryFirstOrDefaultAsync<Master2022>($"SELECT Reference, Name, contact_number, is_registered FROM dmo.master_2022 WHERE Reference = '{accountNumber}'", cancellationToken: cancellationToken);
//                            if (account != null)
//                            {
//                                //If exist, check if already registered
//                                if (account.is_registered == true)
//                                {
//                                    //Check if the registration is the same with the current phone number
//                                    if (message.MessageFrom == account.contact_number)
//                                    {
//                                        _jobService.Enqueue(() => _smsService.SmsSend(message.MessageFrom, $"Account {accountNumber} already registered.", false, false, "sms.automatic"));
//                                    }
//                                    else
//                                    {
//                                        //account already registered with different mobile number
//                                        _jobService.Enqueue(() => _smsService.SmsSend(message.MessageFrom, $"Account {accountNumber} already registered with Mobile DocumentDate {account.contact_number}", false, false, "sms.automatic"));
//                                    }
//                                }
//                                else
//                                {
//                                    //register account here
//                                    await _repoRating.ExecuteScalarAsync($"UPDATE dmo.master_2022 SET contact_number = '{message.MessageFrom}', is_registered = 1, date_registered = CURRENT_TIMESTAMP() WHERE Reference = '{accountNumber}'", cancellationToken: cancellationToken);

//                                    _jobService.Enqueue(() => _smsService.SmsSend(message.MessageFrom, $"Congratulations! Your Account {accountNumber} {account.Name} has successfully registered to the 39th ZANECO Virtual AGMA to be conducted on December 17, 2022 via ZOOM, Facebook Live and YouTube Live.", true, false, "sms.automatic"));
//                                }
//                            }
//                            else
//                            {
//                                //account not exist
//                                _jobService.Enqueue(() => _smsService.SmsSend(message.MessageFrom, $"Account {accountNumber} not exist. Please verify your Account and try again.", false, false, "sms.automatic"));
//                            }

//                            _jobService.Enqueue(() => _smsService.SmsRead(message.Id));
//                        }
//                    }

//                    //_jobService.Enqueue(() => _smsService.SmsRead(message.Id));
//                }

//                return true;
//            }
//        }
//        else
//        {
//            await _repoRating.ExecuteScalarAsync<MessageIn>($"UPDATE datazaneco.MessageIn SET IsRead = 1, ReadOn = CURRENT_TIMESTAMP() WHERE MessageFrom LIKE '{request.MessageFrom}' AND IsRead LIKE 0", cancellationToken: cancellationToken);

//            return true;
//        }

//        return false;
//    }
//}