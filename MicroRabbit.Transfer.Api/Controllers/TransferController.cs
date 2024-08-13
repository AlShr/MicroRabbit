using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Transfer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TransferController : ControllerBase
  {
    private readonly ITransferService transferService;

    public TransferController(ITransferService transferService)
    {
      this.transferService = transferService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TransferLog>> Get()
    {
      return Ok(this.transferService.GetTransferLogs());
    }
  }
}
