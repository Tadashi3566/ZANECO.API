namespace ZANECO.API.Application.SMS.Registrations;

public class Master2022Dto : IDto
{
    public string? AccountNumber { get; set; }
    public string? area { get; set; }
    public string? areastr { get; set; }
    public string? District { get; set; }
    public string? Name { get; set; }
    public string? contactnumber { get; set; }
    public bool? isregistered { get; set; }
    public string? verificationcode { get; set; }
}