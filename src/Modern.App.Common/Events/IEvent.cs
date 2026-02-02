using MediatR;

namespace Modern.App.Common.Events;

public interface IEvent : INotification;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> 
    where TEvent : IEvent;