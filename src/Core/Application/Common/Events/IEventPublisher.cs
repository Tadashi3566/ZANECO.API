using ZANECO.API.Shared.Events;

namespace ZANECO.API.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}