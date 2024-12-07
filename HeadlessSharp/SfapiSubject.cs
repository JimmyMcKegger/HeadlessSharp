using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

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
    
    public static SfapiSubject GetInstance(string ApiKey = "", string Domain = "", string ApiVersion = "")
    {
        if (_instance == null)
        {
            // make a singleton because there is only one API
            _instance = new SfapiSubject(ApiKey, Domain, ApiVersion);
        }
        return _instance;
    }
    
    //  private constructor
    private SfapiSubject(string ApiKey, string Domain, string ApiVersion)
    {
        graphqlClient = new GraphQLHttpClient($"https://{Domain}/api/{ApiVersion}/graphql.json", new NewtonsoftJsonSerializer());
        graphqlClient.HttpClient.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token", ApiKey);
    }
    
    public async Task GetShopInfo()
    {
        GraphQLRequest shopQuery = GraphQLQueries.GetHomePageData();
        var response = await graphqlClient.SendQueryAsync<dynamic>(shopQuery);
        Console.WriteLine($"RESPONSE DATA\n{response.Data}");
        JObject shopInfo = response.Data;
        // Console.WriteLine($"Shop Info Type: {shopInfo.GetType().Name}");
        NotifyObservers(shopInfo);
        
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
        try
        {
            Console.WriteLine($"REMOVING OBSERVER: {observer}");
            observers.Remove(observer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR IN UNREGISTER: {e}");
        }
    }

    public void NotifyObservers(JObject data)
    {
        foreach (var observer in observers)
        {
            observer.Update(data);
        }
    }
}