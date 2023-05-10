using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? RateId { get; set; }
}

public class RatingTemplateExportRequestHandler : IRequestHandler<RatingTemplateExportRequest, Stream>
{
    private readonly IReadRepository<RatingTemplate> _repository;
    private readonly IExcelWriter _excelWriter;

    public RatingTemplateExportRequestHandler(IReadRepository<RatingTemplate> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(RatingTemplateExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new RatingTemplateExportWithRateSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class RatingTemplateExportWithRateSpecification : EntitiesByBaseFilterSpec<RatingTemplate, RatingTemplateExportDto>
{
    public RatingTemplateExportWithRateSpecification(RatingTemplateExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Rate)
            .Where(p => p.RateId.Equals(request.RateId!.Value), request.RateId.HasValue);
}