using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.SMS.Registrations;

public class RegistrationGetRequest : IRequest<Master2022Dto>
{
    public string Account { get; set; }

    public RegistrationGetRequest(string account) => Account = account;
}

public class RegistrationGetRequestHandler : IRequestHandler<RegistrationGetRequest, Master2022Dto>
{
    private readonly IRepositoryWithEvents<Master2022> _repository;
    private readonly IStringLocalizer<RegistrationGetRequestHandler> _localizer;

    public RegistrationGetRequestHandler(IRepositoryWithEvents<Master2022> repository, IStringLocalizer<RegistrationGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Master2022Dto> Handle(RegistrationGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new RegistrationByAccountSpec(request.Account), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Registration not found."], request.Account));
}