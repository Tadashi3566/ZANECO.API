namespace ZANECO.API.Application.SMS.MessageIns;

public class AgmaDto : IDto
{
    public int Id { get; }
    public string? District { get; }
    public string? AccountNumber { get; }
    public string? contactnumber { get; }
    public bool? isregistered { get; }
}