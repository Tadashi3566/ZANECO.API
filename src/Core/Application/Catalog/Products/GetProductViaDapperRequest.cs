﻿namespace ZANECO.API.Application.Catalog.Products;

public class GetProductViaDapperRequest : IRequest<ProductDto>
{
    public DefaultIdType Id { get; set; }

    public GetProductViaDapperRequest(DefaultIdType id) => Id = id;
}

public class GetProductViaDapperRequestHandler : IRequestHandler<GetProductViaDapperRequest, ProductDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetProductViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetProductViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ProductDto> Handle(GetProductViaDapperRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.QueryFirstOrDefaultAsync<Product>(
            $"SELECT * FROM Catalog.\"Products\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = product ?? throw new NotFoundException($"Product {request.Id} not found.");

        // Using mapster here throws a null reference exception because of the "BrandName" property
        // in ProductDto and the product not having a Brand assigned.
        return new ProductDto
        {
            Id = product.Id,
            BrandId = product.BrandId,
            BrandName = string.Empty,
            Description = product.Description!,
            ImagePath = product.ImagePath,
            Name = product.Name,
            Rate = product.Rate
        };
    }
}