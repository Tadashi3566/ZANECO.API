namespace ZANECO.API.Application.SMS.MessageOuts;

public class MessageOutDto : BaseDto<int>, IDto
{
    public bool IsAPI { get; set; } = default!;
    public string MessageType { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
}