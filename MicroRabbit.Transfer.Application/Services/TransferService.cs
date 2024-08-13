using MicroRabbit.Domain.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Services
{
  public class TransferService : ITransferService
  {
    private readonly ITransferRepository transferRepository;
    private readonly IEventBus eventBus;

    public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
    {
      this.transferRepository = transferRepository;
      this.eventBus = eventBus;
    }

    public IEnumerable<TransferLog> GetTransferLogs()
    {
      return this.transferRepository.GetTransferLogs();
    }
  }
}
