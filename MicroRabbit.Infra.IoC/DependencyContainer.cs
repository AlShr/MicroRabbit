using MicroRabbit.Domain.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace MicroRabbit.Infra.IoC
{
  public class DependencyContainer
  {
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
      // Domain Bus
      serviceCollection.AddTransient<IEventBus, RabbitMQBus>();
    }
  }
}
