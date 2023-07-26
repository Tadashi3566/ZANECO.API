using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class PayrollAdjustmentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search Payroll Adjustments using available filters.", "")]
    public Task<PaginationResponse<PayrollAdjustmentDto>> SearchAsync(PayrollAdjustmentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Payroll Adjustment details.", "")]
    public Task<PayrollAdjustmentDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new PayrollAdjustmentGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new Payroll Adjustment.", "")]
    public Task<DefaultIdType> CreateAsync(PayrollAdjustmentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a Payroll Adjustment.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(PayrollAdjustmentUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a Payroll Adjustment.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new PayrollAdjustmentDeleteRequest(id));
    }
}