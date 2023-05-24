using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageIns;

public class MessageInUpdateRequest : IRequest<int>
{
    public int Id { get; set; }
    public string MessageFrom { get; set; } = default!;
    public string MessageTo { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class MessageInUpdateRequestHandler : IRequestHandler<MessageInUpdateRequest, int>
{
    private readonly IDapperRepository _repository;

    public MessageInUpdateRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<int> Handle(MessageInUpdateRequest request, CancellationToken cancellationToken)
    {
        await _repository.ExecuteScalarAsync<MessageIn>($"UPDATE datazaneco.MessageIn SET Description = '{request.Description}', Notes = '{request.Notes}' WHERE Id LIKE {request.Id}", cancellationToken: cancellationToken);

        return request.Id;
    }
}