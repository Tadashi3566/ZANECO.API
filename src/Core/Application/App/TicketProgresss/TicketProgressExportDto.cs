namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressExportDto : DtoExtension<TicketProgressExportDto>, IDto
{
    public string TicketCode { get; set; } = default!;
    public string TicketName { get; set; } = default!;

}