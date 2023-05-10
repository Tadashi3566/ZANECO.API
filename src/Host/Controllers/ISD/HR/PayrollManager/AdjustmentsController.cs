using ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class AdjustmentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search Adjustments using available filters.", "")]
    public Task<PaginationResponse<AdjustmentDto>> SearchAsync(AdjustmentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Adjustment details.", "")]
    public Task<AdjustmentDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new AdjustmentGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new Adjustment.", "")]
    public Task<DefaultIdType> CreateAsync(AdjustmentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a Adjustment.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(AdjustmentUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a Adjustment.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new AdjustmentDeleteRequest(id));
    }
}