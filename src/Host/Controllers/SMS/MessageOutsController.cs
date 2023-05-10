using ZANECO.API.Application.SMS.MessageOuts;

namespace ZANECO.API.Host.Controllers.SMS;

public class MessageOutsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SMS)]
    [OpenApiOperation("Search Messages using available filters.", "")]
    public Task<PaginationResponse<MessageOutDto>> SearchAsync(MessageOutSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(FSHAction.View, FSHResource.SMS)]
    [OpenApiOperation("Get Message details.", "")]
    public Task<MessageOutDto> GetAsync(int id)
    {
        return Mediator.Send(new MessageOutGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.SMS)]
    [OpenApiOperation("Create a new MessageOut.", "")]
    public Task<int> CreateAsync(MessageOutCreateRequest request)
    {
        return Mediator.Send(request);
    }
}