namespace ZANECO.API.Application.Common.Mailing;

public interface IMailService : ITransientService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}