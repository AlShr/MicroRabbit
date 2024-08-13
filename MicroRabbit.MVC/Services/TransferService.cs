using MicroRabbit.MVC.Models.DTO;
using Newtonsoft.Json;

namespace MicroRabbit.MVC.Services
{
  public class TransferService : ITransferService
  {
    private readonly HttpClient apiClient;

    public TransferService(HttpClient apiClient)
    {
      this.apiClient = apiClient;
    }

    public async Task Transfer(TransferDto transferDto)
    {
      var uri = "https://localhost:7075/api/Banking";
      var transferContent = new StringContent(JsonConvert.SerializeObject(transferDto), System.Text.Encoding.UTF8, "application/json");

      var response = await apiClient.PostAsync(uri, transferContent);
      response.EnsureSuccessStatusCode();
    }
  }
}
