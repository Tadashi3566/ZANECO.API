using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.App.TicketProgresss;

namespace ZANECO.API.Host.Controllers.App;

[EnableRateLimiting("fixed")]
public class TicketProgressController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Tickets)]
    [OpenApiOperation("Search Ticket Progress using available filters.", "")]
    public Task<PaginationResponse<TicketProgressDto>> SearchAsync(TicketProgressearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Tickets)]
    [OpenApiOperation("Get Ticket Progress details.", "")]
    public Task<TicketProgressDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new TicketProgressGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Tickets)]
    [OpenApiOperation("Get Ticket Progress details via dapper.", "")]
    public Task<TicketProgressDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new TicketProgressGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Tickets)]
    [OpenApiOperation("Create a new Ticket Progress.", "")]
    public Task<Guid> CreateAsync(TicketProgressCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Tickets)]
    [OpenApiOperation("Update a Ticket Progress.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(TicketProgressUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Tickets)]
    [OpenApiOperation("Delete a Ticket Progress.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new TicketProgressDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Tickets)]
    [OpenApiOperation("Export a Ticket Progress.", "")]
    public async Task<FileResult> ExportAsync(TicketProgressExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "TicketProgressExports");
    }
}