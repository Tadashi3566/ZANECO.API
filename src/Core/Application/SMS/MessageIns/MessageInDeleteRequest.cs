using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.SMS.MessageIns;

public class DeleteMessageInRequest : IRequest<int>
{
    public int Id { get; set; }

    public DeleteMessageInRequest(int id) => Id = id;
}

public class MessageInDeleteRequestHandler : IRequestHandler<DeleteMessageInRequest, int>
{
    private readonly IDapperRepository _repository;

    public MessageInDeleteRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<int> Handle(DeleteMessageInRequest request, CancellationToken cancellationToken)
    {
        //var messageIn = await _repoRating.GetByIdAsync(request.Id, cancellationToken);
        //_ = messageIn ?? throw new NotFoundException(_localizer["Message not found."]);
        //await _repoRating.DeleteAsync(messageIn, cancellationToken);

        //Delete via dapper
        await _repository.ExecuteScalarAsync<MessageIn>($"UPDATE datazaneco.MessageIn SET DeletedBy = '86fae5d1-bd24-470f-9dfc-af5e7ada8bd2', DeletedOn = CURRENT_TIMESTAMP() WHERE Id = {request.Id}", cancellationToken);

        return request.Id;
    }
}