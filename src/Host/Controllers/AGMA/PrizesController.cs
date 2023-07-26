﻿using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.AGMA.Prizes;

namespace ZANECO.API.Host.Controllers.AGMA;

[EnableRateLimiting("fixed")]
public class PrizesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Raffles)]
    [OpenApiOperation("Search Prizes using available filters.", "")]
    public Task<PaginationResponse<PrizeDto>> SearchAsync(PrizeSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Raffles)]
    [OpenApiOperation("Get Raffle details.", "")]
    public Task<PrizeDto> GetAsync(Guid id)
    {
        return Mediator.Send(new PrizeGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Raffles)]
    [OpenApiOperation("Create a new Raffle.", "")]
    public Task<Guid> CreateAsync(PrizeCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Raffles)]
    [OpenApiOperation("Update a Raffle.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(PrizeUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Raffles)]
    [OpenApiOperation("Delete a Raffle.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new PrizeDeleteRequest(id));
    }
}