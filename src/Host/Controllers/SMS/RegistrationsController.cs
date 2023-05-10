using ZANECO.API.Application.SMS.Registrations;

namespace ZANECO.API.Host.Controllers.SMS;

public class RegistrationsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Registrations using available filters.", "")]
    public Task<PaginationResponse<Master2022Dto>> SearchAsync(RegistrationSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{account}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Registration details.", "")]
    public Task<Master2022Dto> GetAsync(string account)
    {
        return Mediator.Send(new RegistrationGetViaDapperRequest(account));
    }

    //[HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    //[OpenApiOperation("Create a new Registration.", "")]
    //public Task<Guid> CreateAsync(RegistrationCreateRequest request)
    //{
    //    return Mediator.Send(request);
    //}

    //[HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    //[OpenApiOperation("Update a Registration.", "")]
    //public async Task<ActionResult<Guid>> UpdateAsync(RegistrationUpdateRequest request, Guid id)
    //{
    //    return id != request.Id
    //        ? BadRequest()
    //        : Ok(await Mediator.Send(request));
    //}

    //[HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    //[OpenApiOperation("Delete a Registration.", "")]
    //public Task<Guid> DeleteAsync(Guid id)
    //{
    //    return Mediator.Send(new DeleteRegistrationRequest(id));
    //}
}