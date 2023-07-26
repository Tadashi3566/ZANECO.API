using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.SMS.MessageIns;

namespace ZANECO.API.Host.Controllers.SMS;

[EnableRateLimiting("fixed")]
public class MessageInsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SMS)]
    [OpenApiOperation("Search Inbox Message using available filters.", "")]
    public Task<PaginationResponse<MessageInDto>> SearchAsync(MessageInSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(FSHAction.View, FSHResource.SMS)]
    [OpenApiOperation("Get Inbox Message details.", "")]
    public Task<MessageInDto> GetAsync(int id)
    {
        return Mediator.Send(new MessageInGetRequest(id));
    }

    // [HttpPost]
    // [MustHavePermission(FSHAction.Create, FSHResource.SMS)]
    // [OpenApiOperation("Create a new MessageIn.", "")]
    // public Task<int> CreateAsync(MessageInCreateRequest request)
    // {
    //    return Mediator.Send(request);
    // }

    [HttpPut("{id:int}")]
    [MustHavePermission(FSHAction.Update, FSHResource.SMS)]
    [OpenApiOperation("Update an Inbox Message.", "")]
    public async Task<ActionResult<int>> UpdateAsync(MessageInUpdateRequest request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("read")]
    [MustHavePermission(FSHAction.Update, FSHResource.SMS)]
    [OpenApiOperation("Read an Inbox Message.", "")]
    public Task<bool> ReadAsync(MessageInReadRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.SMS)]
    [OpenApiOperation("Delete an Inbox Message.", "")]
    public Task<int> DeleteAsync(int id)
    {
        return Mediator.Send(new DeleteMessageInRequest(id));
    }
}