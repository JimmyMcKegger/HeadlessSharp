using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace HeadlessSharp;

// observable storefront API interface
public class SfapiSubject : ISfapiSubject
{
    // fields
    private List<IObserver> observers = new List<IObserver>();
    private readonly GraphQLHttpClient graphqlClient;
    private static SfapiSubject _instance;
    
    // getters and setters
    private string ResponseBody { get; set; }
    public List<IObserver> Observers { get; set; }
    public string CartId { get; set; }
    
    public static SfapiSubject GetInstance(string apiKey = "", string domain = "")
    {
        if (_instance == null)
        {
            // make a singleton because there is only one API
            _instance = new SfapiSubject(apiKey, domain);
        }
        return _instance;
    }
    
    //  private constructor
    private SfapiSubject(string apiKey, string domain)
    {
        graphqlClient = new GraphQLHttpClient($"https://{domain}/api/2024-10/graphql.json", new NewtonsoftJsonSerializer());
        graphqlClient.HttpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", apiKey);
    }
    
    public async Task GetShopInfo()
    {
        GraphQLRequest shopQuery = GraphQLQueries.GetHomePageData();
        var response = await graphqlClient.SendQueryAsync<dynamic>(shopQuery);
        Console.WriteLine($"RESPONSE DATA\n{response.Data}");
        var shopInfo = response.Data;
        NotifyObservers(shopInfo.ToString());
        
    }
    // register with the SfapiSubject using the following method
    public void RegisterObserver(IObserver observer)
    {
        Console.WriteLine($"ADDING OBSERVER: {observer}");
        // TODO: only add once
        observers.Add(observer);
    }
    
    // unregister from the storefront API subject using the following method
    public void UnregisterObserver(IObserver observer)
    {
        Console.WriteLine($"REMOVING OBSERVER: {observer}");
        observers.Remove(observer);
    }

    public void NotifyObservers(string data)
    {
        foreach (var observer in observers)
        {
            observer.Update(data);
        }
    }
}