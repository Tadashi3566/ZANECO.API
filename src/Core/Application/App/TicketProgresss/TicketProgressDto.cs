namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDto : BaseDto, IDto
{
    public DefaultIdType TicketId { get; set; }
    public string TicketCode { get; set; } = default!;
    public string TicketName { get; set; } = default!;

}