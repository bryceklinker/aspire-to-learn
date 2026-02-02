namespace Modern.App.Common.Events;

public interface IEventPublisher
{
    Task Publish(IEvent @event);
}