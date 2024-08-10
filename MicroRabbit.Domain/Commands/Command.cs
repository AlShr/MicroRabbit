using MicroRabbit.Domain.Events;

namespace MicroRabbit.Domain.Commands
{
  public abstract class Command : Message
  {
    protected Command()
    {
      this.Timestamp = DateTime.Now;
    }
    public DateTime Timestamp { get; protected set; }
  }
}
