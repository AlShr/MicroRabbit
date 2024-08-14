using MicroRabbit.MVC.Models.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

namespace MicroRabbit.MVC.Services
{
  public class TransferService : ITransferService
  {
    private readonly IOptionsSnapshot<AppSettings> settings;
    private readonly string remoteServiceBaseUrl;
    private HttpClient apiClient;

    public TransferService(HttpClient apiClient, IOptionsSnapshot<AppSettings> settings)
    {
      this.apiClient = apiClient;
      this.settings = settings;
      this.remoteServiceBaseUrl = $"{settings.Value.BankingUrl}/api/banking";
    }

    public async Task Transfer(TransferDto transferDto)
    {
      var transferContent = new StringContent(JsonConvert.SerializeObject(transferDto), System.Text.Encoding.UTF8, "application/json");

      var response = await apiClient.PostAsync(this.remoteServiceBaseUrl, transferContent);
      response.EnsureSuccessStatusCode();
    }
  }
}
