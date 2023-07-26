using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.App.Tickets;

namespace ZANECO.API.Host.Controllers.App;

[EnableRateLimiting("fixed")]
public class TicketsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Tickets)]
    [OpenApiOperation("Search Tickets using available filters.", "")]
    public Task<PaginationResponse<TicketDto>> SearchAsync(TicketSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Tickets)]
    [OpenApiOperation("Get Ticket details.", "")]
    public Task<TicketDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new TicketGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Tickets)]
    [OpenApiOperation("Get Ticket details via dapper.", "")]
    public Task<TicketDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new TicketGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Tickets)]
    [OpenApiOperation("Create a new Ticket.", "")]
    public Task<Guid> CreateAsync(TicketCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Tickets)]
    [OpenApiOperation("Update a Ticket.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(TicketUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("action")]
    [MustHavePermission(FSHAction.Update, FSHResource.Tickets)]
    [OpenApiOperation("Update a Ticket by progress.", "")]
    public async Task<ActionResult<Guid>> ProgressAsync(TicketProgressRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Tickets)]
    [OpenApiOperation("Delete a Ticket.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new TicketDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Tickets)]
    [OpenApiOperation("Export a Tickets.", "")]
    public async Task<FileResult> ExportAsync(TicketExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "TicketExports");
    }
}