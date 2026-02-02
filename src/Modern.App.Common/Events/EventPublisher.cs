using MediatR;

namespace Modern.App.Common.Events;

internal class EventPublisher(IMediator mediator) : IEventPublisher
{
    public async Task Publish(IEvent @event)
    {
        await mediator.Publish(@event).ConfigureAwait(false);
    }
}