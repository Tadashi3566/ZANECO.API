using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Inventories;

public class InventoryDto : BaseDto, IDto
{
    //public virtual Employee Employee { get; set; } = default!;
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public string MrCode { get; set; } = default!;
    public string ItemCode { get; set; } = default!;
    public DateTime DateReceived { get; set; } = default!;
    public decimal Cost { get; set; } = default!;
}