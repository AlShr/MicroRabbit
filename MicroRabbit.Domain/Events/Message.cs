using MediatR;

namespace MicroRabbit.Domain.Events
{
  public abstract class Message : IRequest<bool>
  {
    protected Message()
    {
      this.MessageType = GetType().Name;
    }
    public string MessageType { get; protected set; }
  }
}
