using MediatR;
using Microsoft.Extensions.Logging;
using ZANECO.API.Application.Common.Events;
using ZANECO.API.Shared.Events;

namespace ZANECO.API.Infrastructure.Common.Services;

public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IPublisher _mediator;

    public EventPublisher(ILogger<EventPublisher> logger, IPublisher mediator) =>
        (_logger, _mediator) = (logger, mediator);

    public Task PublishAsync(IEvent @event)
    {
        string[] eventName = @event.GetType().FullName!.Split(',');
        _logger.LogInformation("Publishing Event : {event}", eventName[0].Trim());
        return _mediator.Publish(CreateEventNotification(@event));
    }

    private static INotification CreateEventNotification(IEvent @event) =>
        (INotification)Activator.CreateInstance(
            typeof(EventNotification<>).MakeGenericType(@event.GetType()), @event)!;
}