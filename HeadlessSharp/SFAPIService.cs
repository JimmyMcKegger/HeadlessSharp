namespace HeadlessSharp;

public class SFAPIService
{
  private readonly IShopifyClient _shopifyClient;
  public SFAPIService(IShopifyClient shopifyClient)
  {
    _shopifyClient = shopifyClient;
  }
  // private readonly IHttpClientFactory _httpClientFactory;
  // private readonly IShopifyClient shopifyClient;
  
  
  public async Task RequestAsync()
  {
    try
    {
      // var result = await _shopifyClient.ExecuteGetShopInfoQueryAsync();
      // if (result.Data?.Shop != null)
      // {
      //   Console.WriteLine($"Shop ID: {result.Data.Shop.Id}");
      //   Console.WriteLine($"Shop Name: {result.Data.Shop.Name}");
      // }
      // else
      // {
      //   Console.WriteLine("No shop data returned.");
      // }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred: {ex.Message}");
    }
  }
}