using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Data.Repository
{
  public class TransferRepository : ITransferRepository
  {
    private readonly TransferDbContext dbContext;

    public TransferRepository(TransferDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public IEnumerable<TransferLog> GetTransferLogs()
    {
      return this.dbContext.TransferLogs;
    }

    public void Add(TransferLog log)
    {
      this.dbContext.TransferLogs.Add(log);
      this.dbContext.SaveChanges();
    }
  }
}
