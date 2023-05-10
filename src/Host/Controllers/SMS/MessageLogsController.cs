using ZANECO.API.Application.SMS.MessageLogs;

namespace ZANECO.API.Host.Controllers.SMS;

public class MessageLogsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SMS)]
    [OpenApiOperation("Search MessageLogs using available filters.", "")]
    public Task<PaginationResponse<MessageLogDto>> SearchAsync(MessageLogSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(FSHAction.View, FSHResource.SMS)]
    [OpenApiOperation("Get MessageLog details.", "")]
    public Task<MessageLogDto> GetAsync(int id)
    {
        return Mediator.Send(new MessageLogGetRequest(id));
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(FSHAction.Update, FSHResource.SMS)]
    [OpenApiOperation("Update a MessageLog.", "")]
    public async Task<ActionResult<int>> UpdateAsync(MessageLogUpdateRequest request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.SMS)]
    [OpenApiOperation("Delete a MessageLog.", "")]
    public Task<int> DeleteAsync(int id)
    {
        return Mediator.Send(new DeleteMessageLogRequest(id));
    }
}