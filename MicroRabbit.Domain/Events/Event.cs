namespace MicroRabbit.Domain.Events
{
  public abstract class Event
  {
    protected Event()
    {
      this.Timestamp = DateTime.Now;
    }
    public DateTime Timestamp { get; protected set; }
  }
}
