using ZANECO.API.Application.SMS.Contacts;

namespace ZANECO.API.Host.Controllers.SMS;

public class ContactsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Contacts)]
    [OpenApiOperation("Search Contacts using available filters.", "")]
    public Task<PaginationResponse<ContactDto>> SearchAsync(ContactSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Contacts)]
    [OpenApiOperation("Get Contact details.", "")]
    public Task<ContactDto> GetAsync(Guid id)
    {
        return Mediator.Send(new ContactGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Contacts)]
    [OpenApiOperation("Create a new Contact.", "")]
    public Task<Guid> CreateAsync(ContactCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("migrate")]
    [MustHavePermission(FSHAction.Create, FSHResource.Contacts)]
    [OpenApiOperation("Migrate Contacts.", "")]
    public Task<Guid> MigrateAsync(ContactMigrateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Contacts)]
    [OpenApiOperation("Update a Contact.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(ContactUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Contacts)]
    [OpenApiOperation("Delete a Contact.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteContactRequest(id));
    }
}