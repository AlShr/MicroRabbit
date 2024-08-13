using MicroRabbit.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MicroRabbit.MVC.Models.DTO;
using MicroRabbit.MVC.Services;

namespace MicroRabbit.MVC.Controllers
{
  public class HomeController : Controller
  {
    private readonly ITransferService transferService;
    private readonly ILogger<HomeController> logger;

    public HomeController(ITransferService transferService, ILogger<HomeController> logger)
    {
      this.transferService = transferService;
      this.logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(TransferViewModel transferViewModel)
    {
      TransferDto transferDto = new TransferDto()
      {
        FromAccount = transferViewModel.FromAccount,
        ToAccount = transferViewModel.ToAccount,
        TransferAmount = transferViewModel.TransferAmount
      };

      await transferService.Transfer(transferDto);
      return View("Index");
    }
  }
}
