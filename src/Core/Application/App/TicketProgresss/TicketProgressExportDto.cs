namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressExportDto : BaseDto, IDto
{
    public string TicketCode { get; set; } = default!;
    public string TicketName { get; set; } = default!;

}