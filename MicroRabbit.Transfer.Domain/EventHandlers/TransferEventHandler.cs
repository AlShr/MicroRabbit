using MicroRabbit.Domain.Bus;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.EventHandlers
{
  public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
  {
    private readonly ITransferRepository transferRepository;

    public TransferEventHandler(ITransferRepository transferRepository)
    {
      this.transferRepository = transferRepository;
    }

    public Task Handler(TransferCreatedEvent @event)
    {
      transferRepository.Add(new TransferLog()
      {
        FromAccount = @event.From,
        ToAccount = @event.To,
        TransferAmount = @event.Amount
      });

      return Task.CompletedTask;
    }
  }
}
