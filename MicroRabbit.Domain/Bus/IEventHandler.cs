using MicroRabbit.Domain.Events;

namespace MicroRabbit.Domain.Bus
{
  public interface IEventHandler<in TEvent>: IEventHandle where TEvent : Event
  {
    Task Handler(TEvent @event);
  }

  public interface IEventHandle
  {
  }
}
