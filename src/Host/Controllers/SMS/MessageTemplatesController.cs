using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.SMS.MessageTemplates;

namespace ZANECO.API.Host.Controllers.SMS;

[EnableRateLimiting("fixed")]
public class MessageTemplatesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.SMS)]
    [OpenApiOperation("Search MessageTemplates using available filters.", "")]
    public Task<PaginationResponse<MessageTemplateDto>> SearchAsync(MessageTemplateSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("interruptions")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Get all incoming power interruptions.", "")]
    public Task<List<MessageTemplateDto>> InterruptionAsync(MessageTemplateInterruptionRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.SMS)]
    [OpenApiOperation("Get MessageTemplate details.", "")]
    public Task<MessageTemplateDto> GetAsync(Guid id)
    {
        return Mediator.Send(new MessageTemplateGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.SMS)]
    [OpenApiOperation("Create a new MessageTemplate.", "")]
    public Task<Guid> CreateAsync(MessageTemplateCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.SMS)]
    [OpenApiOperation("Update a MessageTemplate.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(MessageTemplateUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("sent")]
    [MustHavePermission(FSHAction.Update, FSHResource.SMS)]
    [OpenApiOperation("Set MessageTemplate as sent.", "")]
    public Task<Guid> SentAsync(MessageTemplateSendRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.SMS)]
    [OpenApiOperation("Delete a MessageTemplate.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteMessageTemplateRequest(id));
    }
}