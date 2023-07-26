using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class ContributionController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search Contribution using available filters.", "")]
    public Task<PaginationResponse<ContributionDto>> SearchAsync(ContributionSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Contribution details.", "")]
    public Task<ContributionDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new ContributionGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new Contribution.", "")]
    public Task<DefaultIdType> CreateAsync(ContributionCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a Contribution.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(ContributionUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a Contribution.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new ContributionDeleteRequest(id));
    }
}