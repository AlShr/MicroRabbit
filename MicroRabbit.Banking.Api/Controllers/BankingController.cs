using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BankingController : ControllerBase
  {
    private readonly IAccountService accountService;
    private readonly ILogger<BankingController> logger;


    public BankingController(IAccountService accountService, ILogger<BankingController> logger)
    {
      this.accountService = accountService;
      this.logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Account>> Get()
    {
      return Ok(accountService.GetAccounts());
    }

    [HttpPost]
    public IActionResult Post([FromBody] AccountTransfer accountTransfer)
    {
      this.accountService.Transfer(accountTransfer);
      return Ok(accountTransfer);
    }
  }
}
