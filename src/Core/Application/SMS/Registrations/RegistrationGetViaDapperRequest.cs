using Mapster;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.SMS.Registrations;

public class RegistrationGetViaDapperRequest : IRequest<Master2022Dto>
{
    public string Account { get; set; }

    public RegistrationGetViaDapperRequest(string account) => Account = account;
}

public class RegistrationViaDapperGetRequestHandler : IRequestHandler<RegistrationGetViaDapperRequest, Master2022Dto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RegistrationViaDapperGetRequestHandler> _localizer;

    public RegistrationViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<RegistrationViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    //$"SELECT * FROM datazaneco.\"Registrations\" WHERE \"Id\" = '{request.Account}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);
    public async Task<Master2022Dto> Handle(RegistrationGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.QueryFirstOrDefaultAsync<Master2022>(
        $"SELECT * FROM dmo.master_2022 WHERE accountnumber = '{request.Account}'", cancellationToken: cancellationToken);

        _ = account ?? throw new NotFoundException(string.Format(_localizer["Registration not found."], request.Account));

        return account.Adapt<Master2022Dto>();
    }
}