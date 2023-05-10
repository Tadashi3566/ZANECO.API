namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutDto : IDto
{
    public int Id { get; set; }
    public bool IsAPI { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
}