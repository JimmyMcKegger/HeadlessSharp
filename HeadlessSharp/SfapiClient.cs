namespace HeadlessSharp;
// TODO: implement a request factory
public class SfapiClient
{
  public static async Task NewCart(string input)
  {
    var client = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Post, "https://garrain.myshopify.com/api/2024-10/graphql.json");
    request.Headers.Add("X-Shopify-Storefront-Access-Token", "768cb01e8579585e695152a3b4b89ab0");

    // TODO: Parse request from a GraphQL file and add to string Content
    var content = new StringContent(input, null, "application/json");
    request.Content = content;
    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);

    
  }
}