using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Domain.Bus;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
  public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
  {
    private readonly IEventBus eventBus;

    public TransferCommandHandler(IEventBus eventBus)
    {
      this.eventBus = eventBus;
    }

    public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
      eventBus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));

      return Task.FromResult(true);
    }
  }
}
