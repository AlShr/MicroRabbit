using System.Linq;
using System.Text;
using MediatR;
using MicroRabbit.Domain.Bus;
using MicroRabbit.Domain.Commands;
using MicroRabbit.Domain.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroRabbit.Infra.Bus
{
  public sealed class RabbitMQBus : IEventBus
  {
    private readonly IMediator mediator;
    private readonly Dictionary<string, List<Type>> handlers;
    private readonly List<Type> eventTypes;

    public RabbitMQBus(IMediator mediator)
    {
      this.mediator = mediator;
      this.handlers = new Dictionary<string, List<Type>>();
      this.eventTypes = new List<Type>();
    }

    public Task SendCommand<T>(T command) where T : Command
    {
      return mediator.Send(command);
    }

    public void Publish<T>(T @event) where T : Event
    {
      var factory = new ConnectionFactory() { HostName = "localhost" };
      using (var connection = factory.CreateConnection())
      using (var channel = connection.CreateModel())
      {
        var eventName = @event.GetType().Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", eventName, null, body);
      }
    }

    public void Subscribe<T, TH>()
      where T : Event
      where TH : IEventHandler<T>
    {
      var eventName = typeof(T).Name;
      var handlerType = typeof(TH);

      if (eventTypes.Contains(typeof(T)) == false)
      {
        eventTypes.Add(typeof(T));
      }

      if (handlers.ContainsKey(eventName) == false)
      {
        handlers.Add(eventName, new List<Type>());
      }

      if (handlers[eventName].Any(x => x.GetType() == handlerType))
      {
        throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'");
      }

      handlers[eventName].Add(handlerType);

      this.StartBasicConsume<T>();
    }

    private void StartBasicConsume<T>() where T : Event
    {
      var factory = new ConnectionFactory()
      {
        HostName = "localhost",
        DispatchConsumersAsync = true
      };

      var connection = factory.CreateConnection();
      var channel = connection.CreateModel();

      var eventName = typeof(T).Name;

      channel.QueueDeclare(eventName, false, false, false, null);

      var consumer = new AsyncEventingBasicConsumer(channel);
      consumer.Received += OnReceivedConsumer;

      channel.BasicConsume(eventName, true, consumer);
    }

    private async Task OnReceivedConsumer(object sender, BasicDeliverEventArgs e)
    {
      var eventName = e.RoutingKey;
      var message = Encoding.UTF8.GetString(e.Body.ToArray());

      try
      {
        await ProcessEvent(eventName, message).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
      }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
      if (handlers.ContainsKey(eventName))
      {
        var subscriptions = handlers[eventName];
        foreach (var subscription in subscriptions)
        {
          var handler = Activator.CreateInstance(subscription);
          if (handler == null)
          {
            continue;
          }

          var eventType = eventTypes.SingleOrDefault(x => x.Name == eventName);
          var @event = JsonConvert.DeserializeObject(message, eventType);
          var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
          await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
        }
      }
    }
  }
}
