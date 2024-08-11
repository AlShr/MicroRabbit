using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Data.Repository
{
  public class AccountRepository : IAccountRepository
  {
    private readonly BankingDbContext dbContext;

    public AccountRepository(BankingDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public IEnumerable<Account> GetAccounts()
    {
      return dbContext.Accounts;
    }
  }
}
