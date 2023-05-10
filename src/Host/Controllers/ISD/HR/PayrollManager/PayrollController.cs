using ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class PayrollController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search Payroll using available filters.", "")]
    public Task<PaginationResponse<PayrollDto>> SearchAsync(PayrollSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Payroll details.", "")]
    public Task<PayrollDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new PayrollGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new Payroll.", "")]
    public Task<DefaultIdType> CreateAsync(PayrollCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a Payroll.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(PayrollUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("generate")]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Generate a new Employee Payroll.", "")]
    public Task<bool> GenerateAsync(PayrollGenerateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a Payroll.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new PayrollDeleteRequest(id));
    }
}