namespace HeadlessSharp;

// observable storefront API interface
public class SfapiSubject : ISfapiSubject
{
    // fields
    private List<IObserver> observers = new List<IObserver>();
    private HttpClient _httpClient { get; set; }
    private string ResponseBody { get; set; }
    
    // getters and setters
    public List<IObserver> Observers { get; set; }
    public string CartId { get; set; }

    // make a singleton because there is only one API
    public static readonly SfapiSubject Instance = new SfapiSubject();
    
    //  private constructor
    private SfapiSubject() { }
    
    public async Task GetShopInfo()
    {
        string result = "";
        
        var request = new HttpRequestMessage(HttpMethod.Post, "https://garrain.myshopify.com/api/2024-10/graphql.json");
        request.Headers.Add("X-Shopify-Storefront-Access-Token", "768cb01e8579585e695152a3b4b89ab0");

        // TODO: Parse request from a GraphQL file and add to string Content
        var content = new StringContent("{\"query\":\"query{\\n    shop{\\n        id\\n    }\\n}\",\"variables\":{}}", null, "application/json");
        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        ResponseBody = await response.Content.ReadAsStringAsync();
    }
    // observer will register with the SfapiSubject using the following method
    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    
    // The observer will unregister from the storefront API subject using the following method
    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    

    public void NotifyObservers()
    {
        Console.WriteLine("Notify ");

        foreach (IObserver observer in observers)
        {
            // update the observers
           observer.Update("cartInfo");
        }
    }
}