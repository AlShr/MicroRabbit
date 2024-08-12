using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Bus;

namespace MicroRabbit.Banking.Application.Services
{
  public class AccountService : IAccountService
  {
    private readonly IAccountRepository accountRepository;
    private readonly IEventBus eventBus;


    public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
    {
      this.accountRepository = accountRepository;
      this.eventBus = eventBus;
    }

    public IEnumerable<Account> GetAccounts()
    {
      return this.accountRepository.GetAccounts();
    }

    public void Transfer(AccountTransfer accountTransfer)
    {
      var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount, accountTransfer.ToAccount, accountTransfer.TransferAmount);
      eventBus.SendCommand(createTransferCommand);
    }
  }
}
